using TShip.Models.DTO.Auth;

namespace TShip.Repositories
{
    public interface IAccountRepo
    {
        Task<(bool Success, string? Message, Guid? AccountId)> Register(RegisterRequest request);
    }
}
