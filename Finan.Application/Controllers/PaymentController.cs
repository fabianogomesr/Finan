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
    public class PaymentController : ControllerBase
    {
        private IPaymentService _basePaymentService;

        public PaymentController(IPaymentService basePaymentService)
        {
            _basePaymentService = basePaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PaymentCommand PaymentParameter) => await ExecuteAsync(async () => await _basePaymentService.AddPayment<PaymentValidator>(PaymentParameter));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PaymentCommand PaymentParameter) => await ExecuteAsync(async () => await _basePaymentService.UpdatePayment<PaymentValidator>(PaymentParameter));

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _basePaymentService.GetAsync();

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
                var result = _basePaymentService.GetStatusList();

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
                var result = _basePaymentService.GetTypeList();

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
                var result = await _basePaymentService.GetPaymentsAsync(pageNumber, pageSize);

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
                var result = await _basePaymentService.GetPaymentByIdAsync(id);

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

