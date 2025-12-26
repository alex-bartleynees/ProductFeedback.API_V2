using Application.Abstractions;
using Application.Common;
using Application.Common.Models;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands
{
    public record UploadUserProfileImage(
        Guid UserId,
        Stream FileStream,
        string FileName,
        string ContentType,
        long FileSize
    ) : IRequest<Result<ImageUploadResultDto>>;

    public class UploadUserProfileImageHandler : IRequestHandler<UploadUserProfileImage, Result<ImageUploadResultDto>>
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ISuggestionsRepository _suggestionsRepository;
        private readonly ILogger<UploadUserProfileImageHandler> _logger;

        private static readonly string[] AllowedContentTypes =
            { "image/jpeg", "image/png", "image/gif", "image/webp" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB

        public UploadUserProfileImageHandler(
            IBlobStorageService blobStorageService,
            ISuggestionsRepository suggestionsRepository,
            ILogger<UploadUserProfileImageHandler> logger)
        {
            _blobStorageService = blobStorageService ?? throw new ArgumentNullException(nameof(blobStorageService));
            _suggestionsRepository = suggestionsRepository ?? throw new ArgumentNullException(nameof(suggestionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<ImageUploadResultDto>> Handle(
            UploadUserProfileImage request,
            CancellationToken cancellationToken)
        {
            Guard.Against.Null(request);
            Guard.Against.Default(request.UserId);

            // Validate content type
            if (!AllowedContentTypes.Contains(request.ContentType.ToLowerInvariant()))
            {
                return Result<ImageUploadResultDto>.Failure(
                    new Error(400, "Invalid Content Type",
                        $"Content type must be one of: {string.Join(", ", AllowedContentTypes)}"));
            }

            // Validate file size
            if (request.FileSize <= 0 || request.FileSize > MaxFileSizeBytes)
            {
                return Result<ImageUploadResultDto>.Failure(
                    new Error(400, "Invalid File Size",
                        $"File size must be between 1 byte and {MaxFileSizeBytes / (1024 * 1024)}MB"));
            }

            // Verify user exists
            var userResult = await _suggestionsRepository.GetUser(request.UserId);
            if (userResult.IsFailure)
            {
                return Result<ImageUploadResultDto>.Failure(userResult.Error);
            }

            var user = userResult.Value!;

            // Delete old image if exists (extract blob key from URL)
            if (!string.IsNullOrEmpty(user.Image) && user.Image.Contains("profile-images/"))
            {
                var oldBlobKeyIndex = user.Image.IndexOf("profile-images/");
                var oldBlobKey = user.Image.Substring(oldBlobKeyIndex);
                var deleteResult = await _blobStorageService.DeleteAsync(oldBlobKey, cancellationToken);
                if (deleteResult.IsFailure)
                {
                    _logger.LogWarning("Failed to delete old profile image: {BlobKey}", oldBlobKey);
                    // Continue anyway - old image cleanup is not critical
                }
            }

            // Generate unique blob key
            var fileExtension = Path.GetExtension(request.FileName);
            var blobKey = $"profile-images/{request.UserId}/{Guid.NewGuid()}{fileExtension}";

            // Upload new image
            var uploadResult = await _blobStorageService.UploadAsync(
                request.FileStream,
                blobKey,
                request.ContentType,
                cancellationToken);

            if (uploadResult.IsFailure)
            {
                return Result<ImageUploadResultDto>.Failure(uploadResult.Error);
            }

            // Get URL for the uploaded image
            var urlResult = await _blobStorageService.GetUrlAsync(blobKey, cancellationToken);
            var url = urlResult.IsSuccess ? urlResult.Value! : blobKey;

            // Update user record with full URL
            var updateResult = await _suggestionsRepository.UpdateUserImageAsync(request.UserId, url);
            if (updateResult.IsFailure)
            {
                // Attempt to clean up uploaded blob
                await _blobStorageService.DeleteAsync(blobKey, cancellationToken);
                return Result<ImageUploadResultDto>.Failure(updateResult.Error);
            }

            return Result<ImageUploadResultDto>.Success(new ImageUploadResultDto(blobKey, url));
        }
    }
}
