using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Commands;
using Finan.Service.Services;
using Finan.Service.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Finan.Domain.Parameters;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class GroupController : ControllerBase
    {
        private IBaseService<Group> _baseGroupService;

        public GroupController(IBaseService<Group> baseGroupService)
        {
            _baseGroupService = baseGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GroupCommand GroupParameter)
        {
            return await ExecuteAsync(async () => await _baseGroupService.Add<GroupValidator>(new Group
            {
                Description = GroupParameter.Description,
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] GroupCommand GroupParameter)
        {
            return await ExecuteAsync(async () => await _baseGroupService.UpdateAsync<GroupValidator>(new Group
            {
                Id = GroupParameter.Id,
                Description = GroupParameter.Description,
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseGroupService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() 
        {
            try
            {
                var result = await _baseGroupService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new GroupDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    Nature = (byte)x.Nature
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var result = await _baseGroupService.GetAsync(pageNumber, pageSize);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _baseGroupService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok( new GroupDTO
                {
                    Id = result.Id,
                    Description = result.Description,
                    Nature = (byte)result.Nature
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<IActionResult> ExecuteAsync(Func<Task<object>> func)
        {
            try
            {
                var result = await func();

                if (result == null || result.Equals(string.Empty))
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

