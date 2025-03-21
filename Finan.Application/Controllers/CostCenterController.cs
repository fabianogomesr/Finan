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
    public class CostCenterController : ControllerBase
    {
        private IBaseService<CostCenter> _baseCostCenterService;

        public CostCenterController(IBaseService<CostCenter> baseCostCenterService)
        {
            _baseCostCenterService = baseCostCenterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CostCenterCommand CostCenterParameter)
        {
            return await ExecuteAsync(async () => await _baseCostCenterService.Add<CostCenterValidator>(new CostCenter
            {
                Description = CostCenterParameter.Description,
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CostCenterCommand CostCenterParameter)
        {
            return await ExecuteAsync(async () => await _baseCostCenterService.UpdateAsync<CostCenterValidator>(new CostCenter
            {
                Id = CostCenterParameter.Id,
                Description = CostCenterParameter.Description,
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseCostCenterService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _baseCostCenterService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new CostCenterDTO
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _baseCostCenterService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(new CostCenterDTO
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

