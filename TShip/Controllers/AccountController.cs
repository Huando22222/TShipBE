using CryptoHelper;
using Microsoft.AspNetCore.Mvc;
using TShip.Data;
using TShip.Models.DTO.Auth;
using TShip.Models.Entities;
using TShip.Repositories;

namespace TShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await accountRepo.Register(request);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { Message = "Đăng ký thành công", AccountId = result.AccountId });
        }
    }
}
