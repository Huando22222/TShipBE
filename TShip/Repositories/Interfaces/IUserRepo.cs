using TShip.Models.DTO.RequestDTO.Auth;
using TShip.Models.DTO.Wrappers;

namespace TShip.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<Response<object>> SignUp(Request<SignUpUserRequestDTO> request);
    }
}
