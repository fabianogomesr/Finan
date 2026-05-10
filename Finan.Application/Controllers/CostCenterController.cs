using Finan.Contracts.Request;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CostCenterController : BaseController
    {
        private ICostCenterService _baseCostCenterService;

        public CostCenterController(ICostCenterService baseCostCenterService)
        {
            _baseCostCenterService = baseCostCenterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CostCenterRequest costCenterCommand)
        {
            var response = await _baseCostCenterService.CreateAsync(costCenterCommand);

            return TreatObjectResultCreated(response, _baseCostCenterService.Messages);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CostCenterRequest costCenterCommand)
        {
            var response = await _baseCostCenterService.UpdateAsync(costCenterCommand);

            return TreatObjectResultOk(response, _baseCostCenterService.Messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseCostCenterService.DeleteAsync(id);

            return TreatObjectResultOk(id, _baseCostCenterService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _baseCostCenterService.GetAsync();

            return TreatObjectResultOk(response, _baseCostCenterService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseCostCenterService.GetAsync(pageNumber, pageSize);

            return TreatObjectResultOk(response, _baseCostCenterService.Messages);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseCostCenterService.GetByIdAsync(id);

            return TreatObjectResultOk(response, _baseCostCenterService.Messages);
        }
    }
}

