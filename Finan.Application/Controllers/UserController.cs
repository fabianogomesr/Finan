using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _baseUserService;

        public UserController(IUserService baseUserService)
        {
            _baseUserService = baseUserService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCommand userCommand) 
        {
            var response = await _baseUserService.CreateUser(userCommand);

            return TreatObjectResultCreated(response?.Id, _baseUserService.Messages);
        } 

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UserCommand userCommand)
        {
            var response = await _baseUserService.UpdateUser(userCommand);

            return TreatObjectResultOk(response, _baseUserService.Messages);
        }

        [HttpGet("{login}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(string login) 
        {
            var response = await _baseUserService.GetByUserNameAsync(login);

            return TreatObjectResultOk(response, _baseUserService.Messages);
        } 
    }
}

