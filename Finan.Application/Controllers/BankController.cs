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
    [Authorize]
    public class BankController : ControllerBase
    {
        private IBaseService<Bank> _baseBankService;

        public BankController(IBaseService<Bank> baseBankService)
        {
            _baseBankService = baseBankService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BankCommand BankParameter)
        {
            return await ExecuteAsync(async () => await _baseBankService.Add<BankValidator>(new Bank
            {
                Name = BankParameter.Name,
                Code = BankParameter.Code
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] BankCommand BankParameter)
        {
            return await ExecuteAsync(async () => await _baseBankService.UpdateAsync<BankValidator>(new Bank
            {
                Id = BankParameter.Id,
                Name = BankParameter.Name,
                Code = BankParameter.Code
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseBankService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _baseBankService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new BankDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                }));
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
                var result = await _baseBankService.GetAsync(pageNumber, pageSize);

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
                var result = await _baseBankService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(new BankDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Code = result.Code
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
                return BadRequest(ex.Message);
            }
        }
    }
}

