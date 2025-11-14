using Finan.Domain.Commands;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
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
        public async Task<IActionResult> CreateAsync([FromBody] CostCenterCommand costCenterCommand)
        {
            var response = await _baseCostCenterService.CreateAsync(costCenterCommand);

            return TreatObjectResultCreated(response?.Id, _baseCostCenterService.Messages);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CostCenterCommand costCenterCommand)
        {
            var response = await _baseCostCenterService.UpdateAsync(costCenterCommand);

            return TreatObjectResultCreated(response, _baseCostCenterService.Messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseCostCenterService.DeleteAsync(id);

            return TreatObjectResultCreated(id, _baseCostCenterService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _baseCostCenterService.GetAsync();

            return TreatObjectResultCreated(response, _baseCostCenterService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseCostCenterService.GetAsync(pageNumber, pageSize);

            return TreatObjectResultCreated(response, _baseCostCenterService.Messages);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseCostCenterService.GetByIdAsync(id);

            return TreatObjectResultCreated(response, _baseCostCenterService.Messages);
        }
    }
}

