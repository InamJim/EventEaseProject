using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EventEase.Services.Blob
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;

        public LocalFileUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            string folder = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + fileName;
        }
    }
}