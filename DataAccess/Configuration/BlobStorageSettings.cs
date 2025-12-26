namespace DataAccess.Configuration
{
    public class BlobStorageSettings
    {
        public string ServiceUrl { get; set; } = string.Empty;
        public string PublicUrl { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string BucketName { get; set; } = "user-profile-images";
        public bool ForcePathStyle { get; set; } = true;
    }
}
