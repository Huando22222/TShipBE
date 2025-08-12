using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using TShip.Data;
using TShip.Models.DTO.DTO;
using TShip.Models.DTO.RequestDTO.Auth;
using TShip.Models.DTO.ResponseDTO;
using TShip.Models.DTO.Wrappers;
using TShip.Models.Entities;
using TShip.Repositories.Interfaces;
using TShip.Services.Interfaces;

namespace TShip.Repositories
{
    public class AccountRepo(ApplicationDbContext _dbContext , IJwtService _iJwtService) : IAccountRepo
    {

        public async Task<Response<object>> SignUp(Request<SignUpAccountRequestDTO> request)
        {
            // Check username tồn tại
            if (await _dbContext.Accounts.AnyAsync(a => a.Username == request.Data.Username))
            {
                return new Response<object>
                {
                    Meta = request.Meta,
                    Success = false,
                    Message = "Username đã tồn tại",
                    Data = null
                };
            }

            // Tạo tài khoản mới
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Username = request.Data.Username,
                Password = Crypto.HashPassword(request.Data.Password),
            };

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            return new Response<object>
            {
                Meta = request.Meta,
                Success = true,
                Message = "Đăng ký thành công",
                Data = null
            };
        }

        public async Task<Response<SignInAccountDTO>> SignIn(Request<SignInAccountRequestDTO> request)
        {
            var response = new Response<SignInAccountDTO>
            {
                Meta = request.Meta
            };

            var account = await _dbContext.Accounts
               .Include(a => a.AccountRoles)
               .FirstOrDefaultAsync(a => a.Username == request.Data.Username);

            if (account == null)
            {
                response.Success = false;
                response.Message = "Tài khoản không tồn tại";
                response.Data = null;
                return response;
            }

            bool isValid = Crypto.VerifyHashedPassword(account.Password, request.Data.Password);
            if (!isValid)
            {
                response.Success = false;
                response.Message = "Mật khẩu không đúng";
                response.Data = null;
                return response;
            }

            // Lấy danh sách roles
            List<string> roleNames = account.AccountRoles?
                .Select(r => r.Role.ToString())
                .ToList() ?? [];

            // Chuẩn bị payload cho token
            var payload = new TokenPayLoadDTO
            {
                AccountId = account.Id,
                UserId = account.Id, // Nếu bạn có UserId riêng thì đổi giá trị này
                Username = account.Username,
                Roles = roleNames
            };

            // Tạo token & refresh token
            string newToken = _iJwtService.GenerateToken(payload);
            string newRefreshToken = _iJwtService.GenerateToken(payload  );

            // Cập nhật account
            account.Token = newToken;
            account.RefreshToken = newRefreshToken;
            account.LastLogin = DateTime.UtcNow;
            account.FailedLoginAttempts = 0;

            await _dbContext.SaveChangesAsync();

            var signInAccountDTO = new SignInAccountDTO
            {
                Id = account.Id,
                Username = account.Username,
                IsActive = account.IsActive,
                LastLogin = account.LastLogin,
                FailedLoginAttempts = account.FailedLoginAttempts,
                Token = newToken,
                RefreshToken = newRefreshToken,
                Roles = roleNames
            };

            response.Success = true;
            response.Message = "Đăng nhập thành công";
            response.Data = signInAccountDTO;
            return response;
        }
 
    }
}
