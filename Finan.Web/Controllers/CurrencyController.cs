using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class CurrencyController : BaseController
    {
        private ICurrencyClient _CurrencyClient;

        public CurrencyController(ICurrencyClient CurrencyClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _CurrencyClient = CurrencyClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var response = await _CurrencyClient.GetPageAsync();
            return await TreatResponseAsync(response);
        }

        [HttpGet("Currency/Page/{pageNumber}")]
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
            var response = await _CurrencyClient.GetPageAsync(pageNumber);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> UpdateAsync(CurrencyViewModel currencyViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var currency = new CurrencyRequest
            {
                Id = currencyViewModel.Id,
                Name = currencyViewModel.Name,
                Code = currencyViewModel.Code,
                Symbol = currencyViewModel.Symbol
            };

            var response = await _CurrencyClient.UpdateAsync(currency);

            return await TreatResponseAsync(response, "Index");

        }

        public async Task<IActionResult> CreateAsync(CurrencyViewModel currencyViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var currency = new CurrencyRequest
            {
                Id = 0,
                Name = currencyViewModel.Name,
                Code = currencyViewModel.Code,
                Symbol = currencyViewModel.Symbol
            };

            var response = await _CurrencyClient.CreateAsync(currency);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task DeleteAsync(short id)
        {
            await _CurrencyClient.DeleteAsync(id);
            TempData["Message"] = "Currency delete successfully!";
            Response.Redirect("/Currency/Page/1");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var response = await _CurrencyClient.GetAsync(id);
            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> NewAsync() => View();


    }
}
