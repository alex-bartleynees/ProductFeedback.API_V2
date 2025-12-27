using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Application.Abstractions;
using Application.Common;
using DataAccess.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAccess.Services
{
    public class SeaweedFsBlobStorageService : IBlobStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly BlobStorageSettings _settings;
        private readonly ILogger<SeaweedFsBlobStorageService> _logger;
        private static bool _bucketEnsured = false;
        private static readonly SemaphoreSlim _bucketLock = new(1, 1);

        public SeaweedFsBlobStorageService(
            IAmazonS3 s3Client,
            IOptions<BlobStorageSettings> settings,
            ILogger<SeaweedFsBlobStorageService> logger)
        {
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private async Task EnsureBucketExistsAsync(CancellationToken cancellationToken)
        {
            if (_bucketEnsured) return;

            await _bucketLock.WaitAsync(cancellationToken);
            try
            {
                if (_bucketEnsured) return;

                var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _settings.BucketName);
                if (!bucketExists)
                {
                    await _s3Client.PutBucketAsync(_settings.BucketName, cancellationToken);
                    _logger.LogInformation("Created bucket: {BucketName}", _settings.BucketName);
                }
                _bucketEnsured = true;
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogWarning(ex, "Failed to ensure bucket exists: {BucketName}", _settings.BucketName);
            }
            finally
            {
                _bucketLock.Release();
            }
        }

        public async Task<Result<string>> UploadAsync(
            Stream fileStream,
            string key,
            string contentType,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await EnsureBucketExistsAsync(cancellationToken);

                var request = new PutObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key,
                    InputStream = fileStream,
                    ContentType = contentType
                };

                await _s3Client.PutObjectAsync(request, cancellationToken);

                _logger.LogInformation("Successfully uploaded blob: {BlobKey}", key);
                return Result<string>.Success(key);
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, "Failed to upload blob: {BlobKey}", key);
                return Result<string>.Failure(
                    new Error(500, "Storage Error", $"Failed to upload file: {ex.Message}"));
            }
        }

        public async Task<Result<bool>> DeleteAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = _settings.BucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(request, cancellationToken);

                _logger.LogInformation("Successfully deleted blob: {BlobKey}", key);
                return Result<bool>.Success(true);
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, "Failed to delete blob: {BlobKey}", key);
                return Result<bool>.Failure(
                    new Error(500, "Storage Error", $"Failed to delete file: {ex.Message}"));
            }
        }

        public Task<Result<string>> GetUrlAsync(
            string key,
            CancellationToken cancellationToken = default)
        {
            // Bucket has public read access, return direct URL
            var baseUrl = !string.IsNullOrEmpty(_settings.PublicUrl)
                ? _settings.PublicUrl
                : _settings.ServiceUrl;
            var url = $"{baseUrl.TrimEnd('/')}/{_settings.BucketName}/{key}";
            return Task.FromResult(Result<string>.Success(url));
        }
    }
}
