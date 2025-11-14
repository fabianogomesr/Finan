using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : BaseController
    {
        private IGroupService _baseGroupService;

        public GroupController(IGroupService baseGroupService)
        {
            _baseGroupService = baseGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GroupCommand groupCommand)
        {
            var response = await _baseGroupService.CreateGroup(groupCommand);

            return TreatObjectResultCreated(response?.Id, _baseGroupService.Messages);
        }
            


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] GroupCommand groupCommand)
        {
            var response = await _baseGroupService.UpdateGroup(groupCommand);

            return TreatObjectResultCreated(response, _baseGroupService.Messages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _baseGroupService.DeleteAsync(id);

            return TreatObjectResultCreated(id, _baseGroupService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() 
        {
            var response = await _baseGroupService.GetAsync();

            return TreatObjectResultCreated(response, _baseGroupService.Messages);
        }

        [HttpGet("Nature/{natureId}")]
        public async Task<IActionResult> GetGroupsByNatureId(NatureGroup natureId)
        {
            var response = await _baseGroupService.GetGroupsByNatureId(natureId);

            return TreatObjectResultCreated(response, _baseGroupService.Messages);
        }

        [HttpGet("Natures")]
        public IActionResult GetNatureList()
        {
            var response = _baseGroupService.GetNatureList();

            return TreatObjectResultCreated(response, _baseGroupService.Messages);
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _baseGroupService.GetGroupsAsync(pageNumber, pageSize);

            return TreatObjectResultCreated(response, _baseGroupService.Messages);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseGroupService.GetAsync(id);

            return TreatObjectResultCreated(response, _baseGroupService.Messages);
        }
    }
}

