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
        public async Task<IActionResult> CreateAsync([FromBody] ReceivableCommand ReceivableParameter) => await ExecuteAsync(async () => await _baseReceivableService.AddReceivable<ReceivableValidator>(ReceivableParameter));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ReceivableCommand ReceivableParameter) => await ExecuteAsync(async () => await _baseReceivableService.UpdateReceivable<ReceivableValidator>(ReceivableParameter));

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _baseReceivableService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetStatusList")]
        public IActionResult GetStatusList()
        {
            try
            {
                var result = _baseReceivableService.GetStatusList();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetTypeList")]
        public IActionResult GetTypeList()
        {
            try
            {
                var result = _baseReceivableService.GetTypeList();

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
                var result = await _baseReceivableService.GetReceivablesAsync(pageNumber, pageSize);

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

