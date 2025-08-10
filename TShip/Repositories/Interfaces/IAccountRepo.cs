using TShip.Models.DTO.RequestDTO.Auth;
using TShip.Models.DTO.ResponseDTO;
using TShip.Models.DTO.Wrappers;

namespace TShip.Repositories.Interfaces
{
    public interface IAccountRepo
    {
        Task<Response<object>> SignUp(Request<SignUpAccountRequestDTO> request);
        Task<Response<SignInAccountDTO>> SignIn(Request<SignInAccountRequestDTO> request);
    }
}
