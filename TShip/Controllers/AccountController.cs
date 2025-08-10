using CryptoHelper;
using Microsoft.AspNetCore.Mvc;
using TShip.Data;
using TShip.Models.DTO.Auth;
using TShip.Models.DTO.Wrappers;
using TShip.Models.Entities;
using TShip.Repositories;

namespace TShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountRepo iAccountRepo) : ControllerBase //CustomeController
    {
        private readonly IAccountRepo iAccountRepo = iAccountRepo;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Request<RegisterRequest> request)
        {
            var response = await iAccountRepo.Register(request);
            return Ok(response);
        }
    }
}
