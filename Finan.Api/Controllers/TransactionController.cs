using Finan.Contracts.Filters;
using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : BaseController
    {
        private ITransactionService _baseTransactionService;

        public TransactionController(ITransactionService baseTransactionService)
        {
            _baseTransactionService = baseTransactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TransactionRequest transactionCommand)
        {
            var response = await _baseTransactionService.AddTransaction(transactionCommand);

            return TreatObjectResultCreated(response, _baseTransactionService.Messages);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TransactionRequest transactionCommand) 
        {
            var response = await _baseTransactionService.UpdateTransaction(transactionCommand);

            return TreatObjectResultOk(response, _baseTransactionService.Messages);
        }

        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            var response = _baseTransactionService.GetStatusList();

            return TreatObjectResultOk(response, _baseTransactionService.Messages);
        }

        [HttpGet("Types")]
        public IActionResult GetTypes()
        {
            var response = _baseTransactionService.GetTypeList();

            return TreatObjectResultOk(response, _baseTransactionService.Messages);
        }

        [HttpGet("DateTypes")]
        public IActionResult GetDateTypes()
        {
            var response = _baseTransactionService.GetDateTypeList();

            return TreatObjectResultOk(response, _baseTransactionService.Messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]TransactionFilter filter)
        {
            var response = await _baseTransactionService.GetTransactionsAsync(filter);

            return TreatObjectResultOk(response, _baseTransactionService.Messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _baseTransactionService.GetTransactionByIdAsync(id);

            return TreatObjectResultOk(response, _baseTransactionService.Messages);
        }
    }
}

