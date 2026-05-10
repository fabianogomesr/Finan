using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class BankController : BaseController
    {
        private IBankClient _BankClient;

        public BankController(IBankClient BankClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _BankClient = BankClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _BankClient.GetPageAsync();
            return await TreatResponseAsync(response);
        }

        [HttpGet("Bank/Page/{pageNumber}")]
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
            var response = await _BankClient.GetPageAsync(pageNumber);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> UpdateAsync(BankViewModel bankViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var bank = new BankRequest
            {
                Id = bankViewModel.Id,
                Name = bankViewModel.Name,
                Code = bankViewModel.Code
            };

            var response = await _BankClient.UpdateAsync(bank);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task<IActionResult> CreateAsync(BankViewModel bankViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var bank = new BankRequest
            {
                Id = 0,
                Name = bankViewModel.Name,
                Code = bankViewModel.Code
            };

            var response = await _BankClient.CreateAsync(bank);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task DeleteAsync(short id)
        {
            await _BankClient.DeleteAsync(id);
            TempData["Message"] = "Bank delete successfully!";
            Response.Redirect("/Bank/Page/1");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var response = await _BankClient.GetAsync(id);
            return await TreatResponseAsync(response);
        }

        public IActionResult NewAsync() => View();


    }
}
