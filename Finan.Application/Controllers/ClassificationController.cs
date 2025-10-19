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
    [Authorize]
    public class ClassificationController : ControllerBase
    {
        private IClassificationService _baseClassificationService;

        public ClassificationController(IClassificationService baseClassificationService)
        {
            _baseClassificationService = baseClassificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ClassificationCommand ClassificationParameter) => await ExecuteAsync(async () => await _baseClassificationService.AddClassification<ClassificationValidator>(ClassificationParameter));


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ClassificationCommand ClassificationParameter) => await ExecuteAsync(async () => await _baseClassificationService.UpdateClassification<ClassificationValidator>(ClassificationParameter));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseClassificationService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _baseClassificationService.GetClassificationsAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Group/{groupId}")]
        public async Task<IActionResult> GetClassificationsByGroupIdAsync(int groupId)
        {
            try
            {
                var result = await _baseClassificationService.GetClassificationsByGroupIdAsync(groupId);

                if (result == null || !result.Any())
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var result = await _baseClassificationService.GetClassificationsAsync(pageNumber, pageSize);

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
                var result = await _baseClassificationService.GetClassificationByIdAsync(id);

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

