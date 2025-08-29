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
using Finan.Domain.Filters;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _baseTransactionService;

        public TransactionController(ITransactionService baseTransactionService)
        {
            _baseTransactionService = baseTransactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TransactionCommand TransactionParameter) => await ExecuteAsync(async () => await _baseTransactionService.AddTransaction(TransactionParameter));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TransactionCommand TransactionParameter) => await ExecuteAsync(async () => await _baseTransactionService.UpdateTransaction(TransactionParameter));

        [HttpGet]
        [Route("GetStatusList")]
        public IActionResult GetStatusList()
        {
            try
            {
                var result = _baseTransactionService.GetStatusList();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTypeList")]
        public IActionResult GetTypeList()
        {
            try
            {
                var result = _baseTransactionService.GetTypeList();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDateTypeList")]
        public IActionResult GetDateTypeList()
        {
            try
            {
                var result = _baseTransactionService.GetDateTypeList();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]TransactionFilter filter)
        {
            try
            {
                var result = await _baseTransactionService.GetTransactionsAsync(filter);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _baseTransactionService.GetTransactionByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
        }
    }
}

