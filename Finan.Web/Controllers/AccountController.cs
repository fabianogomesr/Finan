using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private IAccountClient _AccountClient;
        private IBankClient _BankClient;

        public AccountController(IAccountClient AccountClient, IBankClient bankClient,  IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _AccountClient = AccountClient;
            _BankClient = bankClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _AccountClient.GetPageAsync();

            return await TreatResponseAsync(response);
        }

        [HttpGet("Account/Page/{pageNumber}")]
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
            var response = await _AccountClient.GetPageAsync(pageNumber);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> UpdateAsync(AccountViewModel account)
        {
            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var Account = new AccountRequest
            {
                Id = account.Id,
                BankId = account.BankId,
                Name = account.Name,
                Agency = account.Agency,
                Number = account.Number,
                CreditLimit = account.CreditLimit
            };

            var response = await _AccountClient.UpdateAsync(Account);

            return await TreatResponseAsync(response, "Index");

        }

        public async Task<IActionResult> CreateAsync(AccountViewModel account)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var Account = new AccountRequest
            {
                BankId = account.BankId,
                Name = account.Name,
                Agency = account.Agency,
                Number = account.Number,
                CreditLimit = account.CreditLimit
            };

            var response = await _AccountClient.CreateAsync(Account);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task<IActionResult> DeleteAsync(short id)
        {
            await _AccountClient.DeleteAsync(id);
            TempData["Message"] = "Account delete successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var banks = await _BankClient.GetAllAsync();

            ViewBag.Banks = banks.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });

            var response = await _AccountClient.GetAsync(id);

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> NewAsync()
        {
            var banks = await _BankClient.GetAllAsync();
            ViewBag.Banks = banks.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }
    }
}
