using Microsoft.AspNetCore.Http;

namespace EventEase.Services.Blob
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}