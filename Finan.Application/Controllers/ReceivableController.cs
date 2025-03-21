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
    public class ReceivableController : ControllerBase
    {
        private IReceivableService _baseReceivableService;

        public ReceivableController(IReceivableService baseReceivableService)
        {
            _baseReceivableService = baseReceivableService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ReceivableCommand ReceivableParameter)
        {
            return Ok(await _baseReceivableService.AddReceivable(ReceivableParameter));
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateReceivablesAsync([FromBody] List<ReceivableCommand> receivables)
        //{
        //    return Ok(await _baseReceivableService.CreateReceivablesAsync(receivables));
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ReceivableCommand ReceivableParameter)
        {
            return Ok(await _baseReceivableService.UpdateReceivable(ReceivableParameter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _baseReceivableService.GetReceivableByIdAsync(id);

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

