using Finan.Contracts.Request;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Controllers
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
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginCommand) 
        {
            var response = await _tokenService.GenerateTokenAsync(loginCommand);

            return TreatObjectResultOk(response, _tokenService.Messages);
        } 
    }
}
