using TShip.Services.Interfaces;

namespace TShip.Services
{
    public class FileService(IWebHostEnvironment env) : IFileService
    {

        // Lưu file public => wwwroot/uploads/{folderName}
        public async Task<string> SavePublicFileAsync(IFormFile file, string folderName, Guid userId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File không hợp lệ");

            var uploadPath = Path.Combine(env.WebRootPath, "uploads", folderName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = GenerateFileName(userId, file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folderName}/{fileName}";
        }

        // Lưu file private => ngang hàng .sln (ngoài wwwroot)
        public async Task<string> SavePrivateFileAsync(IFormFile file, string folderName, Guid userId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File không hợp lệ");

            var privatePath = Path.Combine(Directory.GetParent(env.ContentRootPath)!.FullName, "uploads", folderName);

            if (!Directory.Exists(privatePath))
                Directory.CreateDirectory(privatePath);

            var fileName = GenerateFileName(userId, file.FileName);
            var filePath = Path.Combine(privatePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Chỉ lưu relative path để DB quản lý
            return Path.Combine(folderName, fileName).Replace("\\", "/");
        }

        public async Task<Stream> GetPrivateFileAsync(string relativePath)
        {
            var fullPath = Path.Combine(Directory.GetParent(env.ContentRootPath)!.FullName, "uploads", relativePath);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Không tìm thấy file");

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;
        }

        public Task DeleteFileAsync(string relativePath, bool isPublic)
        {
            var basePath = isPublic ? env.WebRootPath : Directory.GetParent(env.ContentRootPath)!.FullName;
            var filePath = isPublic
                ? Path.Combine(basePath, relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()))
                : Path.Combine(basePath, "uploads", relativePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        //U12345_20250810_072315_a1b2c3d4.png
        //userId_yyyyMMdd_HHmmss_random 
        private string GenerateFileName(Guid userId, string originalFileName)
        {
            var ext = Path.GetExtension(originalFileName);
            var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
            var random = Guid.NewGuid().ToString("N").Substring(0, 8);
            return $"{userId}_{timestamp}_{random}{ext}";
        }
       
    }
}
