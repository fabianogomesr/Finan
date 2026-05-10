using Finan.Contracts.Request;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService _baseAccountService;
        public AccountController(IAccountService baseAccountService)
        {
            _baseAccountService = baseAccountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AccountRequest accountCommand) 
        {
            var response = await _baseAccountService.CreateAsync(accountCommand);

            return TreatObjectResultCreated(response, _baseAccountService.Messages);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AccountRequest accountCommand)
        {
            var response = await _baseAccountService.UpdateAsync(accountCommand);

            return TreatObjectResultOk(response, _baseAccountService.Messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseAccountService.DeleteAsync(id);

            return TreatObjectResultOk(id, _baseAccountService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _baseAccountService.GetAsync();

            return TreatObjectResultOk(response, _baseAccountService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseAccountService.GetAsync(pageNumber, pageSize);

            return TreatObjectResultOk(response, _baseAccountService.Messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseAccountService.GetByIdAsync(id);

            return TreatObjectResultOk(response, _baseAccountService.Messages);
        }
    }
}

