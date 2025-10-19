using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private IBaseService<Currency> _baseCurrencyService;

        public CurrencyController(IBaseService<Currency> baseCurrencyService)
        {
            _baseCurrencyService = baseCurrencyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CurrencyCommand CurrencyParameter)
        {
            return await ExecuteAsync(async () => await _baseCurrencyService.Add<CurrencyValidator>(new Currency
            {
                Name = CurrencyParameter.Name,
                Code = CurrencyParameter.Code,
                Symbol = CurrencyParameter.Symbol
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CurrencyCommand CurrencyParameter)
        {
            return await ExecuteAsync(async () => await _baseCurrencyService.UpdateAsync<CurrencyValidator>(new Currency
            {
                Name = CurrencyParameter.Name,
                Code = CurrencyParameter.Code,
                Symbol = CurrencyParameter.Symbol
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _baseCurrencyService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _baseCurrencyService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new CurrencyDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Symbol = x.Symbol
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Paged/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var result = await _baseCurrencyService.GetAsync(pageNumber, pageSize);

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
                var result = await _baseCurrencyService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(new CurrencyDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Code = result.Code,
                    Symbol = result.Symbol
                });
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
