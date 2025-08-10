namespace TShip.Services.Interfaces
{
    public interface IFileService
    {
        // Lưu file public (ai cũng có thể truy cập qua URL)
        Task<string> SavePublicFileAsync(IFormFile file, string folderName, Guid userId);

        // Lưu file private (ngoài wwwroot, chỉ truy cập qua API)
        Task<string> SavePrivateFileAsync(IFormFile file, string folderName, Guid userId);

        // Lấy stream file private để trả về qua API
        Task<Stream> GetPrivateFileAsync(string relativePath);

        // Xóa file
        Task DeleteFileAsync(string relativePath, bool isPublic);
    }
}
