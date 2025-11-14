using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _tokenService;

        public AuthController(IAuthService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand loginCommand) 
        {
            var response = await _tokenService.GenerateTokenAsync(loginCommand);

            return TreatObjectResultOk(response, _tokenService.Messages);
        } 
    }
}
