using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using TShip.Data;
using TShip.Models.DTO.Auth;
using TShip.Models.DTO.Wrappers;
using TShip.Models.Entities;

namespace TShip.Repositories
{
    public class AccountRepo(ApplicationDbContext dbContext) : IAccountRepo
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        public async Task<Response<object>> Register(Request<RegisterRequest> request)
        {
            // Check username tồn tại
            if (await dbContext.Accounts.AnyAsync(a => a.Username == request.Data.Username))
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

            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();

            return new Response<object>
            {
                Meta = request.Meta,
                Success = true,
                Message = "Đăng ký thành công",
                Data = null
            };
        }
    }
}
