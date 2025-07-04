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
    public class PaymentController : ControllerBase
    {
        private IPaymentService _basePaymentService;

        public PaymentController(IPaymentService basePaymentService)
        {
            _basePaymentService = basePaymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PaymentCommand PaymentParameter) => await ExecuteAsync(async () => await _basePaymentService.AddPayment(PaymentParameter));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PaymentCommand PaymentParameter) => await ExecuteAsync(async () => await _basePaymentService.UpdatePayment(PaymentParameter));

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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDateTypeList")]
        public IActionResult GetDateTypeList()
        {
            try
            {
                var result = _basePaymentService.GetDateTypeList();

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
        [Route("GetPaymentSummaryByMonthYear")]
        public async Task<IActionResult> GetPaymentSummaryByMonthYearAsync(int month, int year)
        {
            try
            {
                var result = await _basePaymentService.GetPaymentSummaryByMonthYear(month, year);

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
        [Route("GetPaymentSummaryClassificationByMonthYear")]
        public async Task<IActionResult> GetPaymentSummaryClassificationByMonthYearAsync(int month, int year)
        {
            try
            {
                var result = await _basePaymentService.GetPaymentSummaryClassificationByMonthYear(month, year);

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
        public async Task<IActionResult> GetAsync([FromQuery]PaymentFilter filter)
        {
            try
            {
                var result = await _basePaymentService.GetPaymentsAsync(filter);

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
                var result = await _basePaymentService.GetPaymentByIdAsync(id);

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

