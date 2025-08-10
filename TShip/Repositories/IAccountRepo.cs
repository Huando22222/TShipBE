using TShip.Models.DTO.Auth;
using TShip.Models.DTO.Wrappers;

namespace TShip.Repositories
{
    public interface IAccountRepo
    {
        Task<Response<object>> Register(Request<RegisterRequest> request);
    }
}
