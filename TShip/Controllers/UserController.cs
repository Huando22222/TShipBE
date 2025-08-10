using Microsoft.AspNetCore.Mvc;
using TShip.Models.DTO.RequestDTO.Auth;
using TShip.Models.DTO.Wrappers;
using TShip.Repositories.Interfaces;
using TShip.Services;
using TShip.Services.Interfaces;

namespace TShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepo iUserRepo ) : ControllerBase
    {
        private readonly IUserRepo iUserRepo = iUserRepo;

        [HttpPost("SignUp")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Response<object>>> SignUp([FromBody] Request<SignUpUserRequestDTO> request)
        {
            return Ok(await iUserRepo.SignUp(request));
        }
    }
}
