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
    public class PayerController : ControllerBase
    {
        private IBaseService<Payer> _basePayerService;

        public PayerController(IBaseService<Payer> basePayerService)
        {
            _basePayerService = basePayerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PayerCommand PayerParameter)
        {
            return await ExecuteAsync(async () => await _basePayerService.Add<PayerValidator>(new Payer
            {
                Name = PayerParameter.Name,
            }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PayerCommand PayerParameter)
        {
            return await ExecuteAsync(async () => await _basePayerService.UpdateAsync<PayerValidator>(new Payer
            {
                Id = PayerParameter.Id,
                Name = PayerParameter.Name,
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                await _basePayerService.DeleteAsync(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _basePayerService.GetAsync();

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(result.Select(x => new PayerDTO
                {
                    Id = x.Id,
                    Name = x.Name
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
                var result = await _basePayerService.GetByIdAsync(id);

                if (result == null || result.Equals(string.Empty))
                    return NotFound();

                return Ok(new PayerDTO
                {
                    Id = result.Id,
                    Name = result.Name
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

