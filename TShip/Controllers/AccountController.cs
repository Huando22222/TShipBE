using CryptoHelper;
using Microsoft.AspNetCore.Mvc;
using TShip.Data;
using TShip.Models.DTO.RequestDTO.Auth;
using TShip.Models.DTO.Wrappers;
using TShip.Models.Entities;
using TShip.Repositories.Interfaces;

namespace TShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountRepo iAccountRepo) : ControllerBase //CustomeController
    {
        private readonly IAccountRepo iAccountRepo = iAccountRepo;

        [HttpPost("SignUp")]
        public async Task<ActionResult<Response<object>>> SignUp([FromBody] Request<SignUpAccountRequestDTO> request)
        {
            return Ok(await iAccountRepo.SignUp(request));
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<Response<object>>> SignIn([FromBody] Request<SignInAccountRequestDTO> request)
        {
            return Ok(await iAccountRepo.SignIn(request));
        }
    }
}
