using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class CostCenterController : BaseController
    {
        private ICostCenterClient _costCenterClient;

        public CostCenterController(ICostCenterClient costCenterClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _costCenterClient = costCenterClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _costCenterClient.GetPageAsync();
            return await TreatResponseAsync(response);
        }

        [HttpGet("CostCenter/Page/{pageNumber}")]
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
            var response = await _costCenterClient.GetPageAsync(pageNumber);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> UpdateAsync(CostCenterViewModel costCenterViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var costCenter = new CostCenterRequest
            {
                Id = costCenterViewModel.Id,
                Description = costCenterViewModel.Description,
            };

            var response = await _costCenterClient.UpdateAsync(costCenter);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task<IActionResult> CreateAsync(CostCenterViewModel costCenterViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var costCenter = new CostCenterRequest
            {
                Id = 0,
                Description = costCenterViewModel.Description,
            };

            var response = await _costCenterClient.CreateAsync(costCenter);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task DeleteAsync(short id)
        {
            await _costCenterClient.DeleteAsync(id);
            TempData["Message"] = "CostCenter delete successfully!";
            Response.Redirect("/CostCenter/Page/1");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var response = await _costCenterClient.GetAsync(id);
            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> NewAsync() => View();


    }
}
