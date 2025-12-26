using Application.Common;

namespace Application.Abstractions
{
    public interface IBlobStorageService
    {
        Task<Result<string>> UploadAsync(Stream fileStream, string key, string contentType, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteAsync(string key, CancellationToken cancellationToken = default);
        Task<Result<string>> GetUrlAsync(string key, CancellationToken cancellationToken = default);
    }
}
