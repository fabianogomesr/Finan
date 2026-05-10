using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Finan.Domain.Interfaces;
using Finan.Contracts.Request;

namespace Finan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassificationController : BaseController
    {
        private IClassificationService _baseClassificationService;

        public ClassificationController(IClassificationService baseClassificationService)
        {
            _baseClassificationService = baseClassificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ClassificationRequest ClassificationParameter)
        {
            var response = await _baseClassificationService.AddClassification(ClassificationParameter);

            return TreatObjectResultCreated(response, _baseClassificationService.Messages);
        } 


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ClassificationRequest ClassificationParameter) 
        {
            var response = await _baseClassificationService.UpdateClassification(ClassificationParameter);

            return TreatObjectResultOk(response, _baseClassificationService.Messages);

        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseClassificationService.DeleteAsync(id);

            return TreatObjectResultOk(id, _baseClassificationService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _baseClassificationService.GetClassificationsAsync();

            return TreatObjectResultOk(response, _baseClassificationService.Messages);
        }

        [HttpGet("Group/{groupId}")]
        public async Task<IActionResult> GetClassificationsByGroupIdAsync(int groupId)
        {
            var response = await _baseClassificationService.GetClassificationsByGroupIdAsync(groupId);

            return TreatObjectResultOk(response, _baseClassificationService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseClassificationService.GetClassificationsAsync(pageNumber, pageSize);

            return TreatObjectResultOk(response, _baseClassificationService.Messages);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseClassificationService.GetClassificationByIdAsync(id);

            return TreatObjectResultOk(response, _baseClassificationService.Messages);
        }
    }
}

