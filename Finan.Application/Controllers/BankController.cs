using Finan.Domain.Commands;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankController : BaseController
    {
        private readonly IBankService _baseBankService;
        public BankController(IBankService baseBankService)
        {
            _baseBankService = baseBankService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BankCommand bankCommand)
        {
            var response = await _baseBankService.CreateAsync(bankCommand);

            return TreatObjectResultCreated(response.Id, _baseBankService.Messages);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] BankCommand bankCommand)
        {
            var response = await _baseBankService.UpdateAsync(bankCommand);

            return TreatObjectResultOk(response, _baseBankService.Messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseBankService.DeleteAsync(id);

            return TreatObjectResultOk(id, _baseBankService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _baseBankService.GetAsync();

            return TreatObjectResultOk(response, _baseBankService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseBankService.GetAsync(pageNumber, pageSize);

            return TreatObjectResultOk(response, _baseBankService.Messages);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseBankService.GetByIdAsync(id);

            return TreatObjectResultOk(response, _baseBankService.Messages);
        }
    }
}

