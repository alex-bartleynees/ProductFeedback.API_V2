namespace Application.Common.Models
{
    public record ImageUploadResultDto(
        string BlobKey,
        string Url
    );
}
