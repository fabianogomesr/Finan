using Finan.Contracts.Request;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : BaseController
    {
        private ICurrencyService _baseCurrencyService;

        public CurrencyController(ICurrencyService baseCurrencyService)
        {
            _baseCurrencyService = baseCurrencyService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CurrencyRequest currencyCommand)
        {
            var response = await _baseCurrencyService.CreateAsync(currencyCommand);

            return TreatObjectResultCreated(response, _baseCurrencyService.Messages);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CurrencyRequest currencyCommand)
        {
            var response = await _baseCurrencyService.UpdateAsync(currencyCommand);

            return TreatObjectResultOk(response, _baseCurrencyService.Messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseCurrencyService.DeleteAsync(id);

            return TreatObjectResultOk(id, _baseCurrencyService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _baseCurrencyService.GetAsync();

            return TreatObjectResultOk(response, _baseCurrencyService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseCurrencyService.GetAsync(pageNumber, pageSize);

            return TreatObjectResultOk(response, _baseCurrencyService.Messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseCurrencyService.GetByIdAsync(id);

            return TreatObjectResultOk(response, _baseCurrencyService.Messages);
        }
    }
}
