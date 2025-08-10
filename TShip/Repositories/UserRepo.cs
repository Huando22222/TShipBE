using Microsoft.EntityFrameworkCore;
using TShip.Data;
using TShip.Models.DTO.RequestDTO.Auth;
using TShip.Models.DTO.Wrappers;
using TShip.Models.Entities;
using TShip.Repositories.Interfaces;
using TShip.Services;
using TShip.Services.Interfaces;

namespace TShip.Repositories
{
    public class UserRepo(ApplicationDbContext _dbContext , IFileService _iFileService) : IUserRepo
    {
        //private readonly ApplicationDbContext _dbContext = dbContext;
        //private readonly IFileService _iFileService = iFileService;
        public async Task<Response<object>> SignUp(Request<SignUpUserRequestDTO> request)
        {
            var response = new Response<object>
            {
                Meta = request.Meta
            };

            var data = request.Data;

            // 1. Kiểm tra user tồn tại
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.AccountId == data.AccountId);

            if (existingUser != null)
            {
                response.Success = false;
                response.Message = "User đã tồn tại";
                response.Data = null;
                return response;
            }

            // 2. Tạo user mới
            var newUser = new User
            {
                AccountId = data.AccountId,
                Name = data.Name,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                Gender = data.Gender,
                DateOfBirth = data.DateOfBirth,
                Address = data.Address,
                IdentityNumber = data.IdentityNumber
            };

            // 3. Lưu ảnh nếu có
            if (data.Avatar != null)
            {
                // SavePrivateFileAsync trả về string (hoặc string?)
                string? avatarPath = await _iFileService.SavePrivateFileAsync(
                    data.Avatar,
                    folderName: "avatars",
                    userId: data.AccountId
                );
                // gán an toàn — AvatarPath là string? theo entity trên
                newUser.AvatarUrl = avatarPath;
            }

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            response.Success = true;
            response.Message = "Đăng ký thành công";
            response.Data = null;
            return response;
        }
    }
}
