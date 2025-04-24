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
using Finan.Infra.Data.Repository;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class FinancialClassificationController : ControllerBase
    {
        private IFinancialClassificationService _baseFinancialClassificationService;

        public FinancialClassificationController(IFinancialClassificationService baseFinancialClassificationService)
        {
            _baseFinancialClassificationService = baseFinancialClassificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] FinancialClassificationCommand financialClassificationParameter) => await ExecuteAsync(async () => await _baseFinancialClassificationService.AddFinancialClassification<FinancialClassificationValidator>(financialClassificationParameter));


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] FinancialClassificationCommand financialClassificationParameter) => await ExecuteAsync(async () => await _baseFinancialClassificationService.UpdateFinancialClassification<FinancialClassificationValidator>(financialClassificationParameter));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseFinancialClassificationService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _baseFinancialClassificationService.GetFinancialClassificationsAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetClassificationsFromReceivableByGroupIdAsync/{financialGroupId}")]
        public async Task<IActionResult> GetClassificationsFromReceivableByGroupIdAsync(int financialGroupId)
        {
            try
            {
                var result = await _baseFinancialClassificationService.GetClassificationsFromReceivableByGroupIdAsync(financialGroupId);

                if (result == null || !result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetClassificationTypeList")]
        public IActionResult GetClassificationTypeList()
        {
            try
            {
                var result = _baseFinancialClassificationService.GetClassificationTypeList();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
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
                var result = await _baseFinancialClassificationService.GetFinancialClassificationsAsync(pageNumber, pageSize);

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
                var result = await _baseFinancialClassificationService.GetFinancialClassificationByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
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

