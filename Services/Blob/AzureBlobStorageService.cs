using Microsoft.AspNetCore.Http;

namespace EventEase.Services.Blob
{
    public class AzureBlobStorageService : IFileUploadService
    {
        public Task<string> UploadFileAsync(IFormFile file)
        {
            // PLACEHOLDER FOR AZURE BLOB IMPLEMENTATION

            // Later we connect:
            // - Azure.Storage.Blobs
            // - Container client
            // - Upload stream

            return Task.FromResult("/uploads/placeholder.png");
        }
    }
}