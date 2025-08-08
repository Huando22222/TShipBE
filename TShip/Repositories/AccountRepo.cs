using CryptoHelper;
using TShip.Data;
using TShip.Models.DTO.Auth;
using TShip.Models.Entities;

namespace TShip.Repositories
{
    public class AccountRepo: IAccountRepo
    {
        private readonly ApplicationDbContext dbContext;

        public AccountRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(bool Success, string? Message, Guid? AccountId)> Register(RegisterRequest request)
        {
            if (dbContext.Accounts.Any(a => a.Username == request.Username))
                return (false, "username đã tồn tại", null);

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = Crypto.HashPassword(request.Password),
                UserId = Guid.Empty
            };

            dbContext.Accounts.Add(account);
            await dbContext.SaveChangesAsync();
            return (true, null, account.Id);
        }
    }
}
