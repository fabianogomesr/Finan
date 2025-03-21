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
    public class FinancialGroupController : ControllerBase
    {
        private IBaseService<FinancialGroup> _baseFinancialGroupService;

        public FinancialGroupController(IBaseService<FinancialGroup> baseFinancialGroupService)
        {
            _baseFinancialGroupService = baseFinancialGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] FinancialGroupCommand financialGroupParameter)
        {
            return await ExecuteAsync(async () => await _baseFinancialGroupService.Add<FinancialGroupValidator>(new FinancialGroup
            {
                Description = financialGroupParameter.Description,
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] FinancialGroupCommand financialGroupParameter)
        {
            return await ExecuteAsync(async () => await _baseFinancialGroupService.UpdateAsync<FinancialGroupValidator>(new FinancialGroup
            {
                Id = financialGroupParameter.Id,
                Description = financialGroupParameter.Description,
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseFinancialGroupService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() 
        {
            try
            {
                var result = await _baseFinancialGroupService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new FinancialGroupDTO
                {
                    Id = x.Id,
                    Description = x.Description
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
                var result = await _baseFinancialGroupService.GetAsync(pageNumber, pageSize);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(new FinancialGroupPaginationDTO
                {
                    FinancialGroups = result.Entities.Select(x => new FinancialGroupDTO
                    {
                        Id = x.Id,
                        Description = x.Description,
                    }).ToList(),
                    CurrentPage = result.CurrentPage,
                    TotalItems = result.TotalItems,
                    TotalPages = result.TotalPages
                });
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
                var result = await _baseFinancialGroupService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok( new FinancialGroupDTO
                {
                    Id = result.Id,
                    Description = result.Description
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

