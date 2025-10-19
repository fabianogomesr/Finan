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
using Finan.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private IGroupService _baseGroupService;

        public GroupController(IGroupService baseGroupService)
        {
            _baseGroupService = baseGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GroupCommand groupCommand) => await ExecuteAsync(async () => await _baseGroupService.CreateGroup(groupCommand));


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] GroupCommand groupCommand)
        {
            return await ExecuteAsync(async () => await _baseGroupService.UpdateGroup(groupCommand));
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
                    Nature = x.Nature.GetDescription(),
                    NatureId = (byte)x.Nature
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Nature/{natureId}")]
        public async Task<IActionResult> GetGroupsByNatureId(NatureGroup natureId)
        {
            try
            {
                var result = await _baseGroupService.GetAll().Where(x => x.Nature == natureId).ToListAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new GroupDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    Nature = x.Nature.GetDescription(),
                    NatureId = (byte)x.Nature
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Natures")]
        public IActionResult GetNatureList()
        {
            var result = EnumExtensions.GetEnumList<NatureGroup>();

            return Ok(result.Select(x => new NatureDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList());
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var result = await _baseGroupService.GetGroupsAsync(pageNumber, pageSize);

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
                    Nature = result.Nature.GetDescription(),
                    NatureId = (byte)result.Nature
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

