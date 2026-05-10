using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class GroupController : BaseController
    {
        private IGroupClient _GroupClient;
        private IHttpContextAccessor _httpContextAccessor;
        private const int _pageSize = 5;

        public GroupController(IGroupClient GroupClient, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _GroupClient = GroupClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _GroupClient.GetPageAsync();

            return await TreatResponseAsync(response);
        }

        [HttpGet("Group/Page/{pageNumber}")]
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
            var response = await _GroupClient.GetPageAsync(pageNumber, _pageSize);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> UpdateAsync(GroupViewModel groupViewModel)
        {

            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var response = await _GroupClient.UpdateAsync(new GroupRequest
            {
                Id = groupViewModel.Id,
                Description = groupViewModel.Description,
                NatureId = (Contracts.Enums.NatureGroup)groupViewModel.NatureId,
            });

            return await TreatResponseAsync(response, "Index");

        }

        public async Task<IActionResult> CreateAsync(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var response = await _GroupClient.CreateAsync(new GroupRequest
            {
                Description = groupViewModel.Description,
                NatureId = (Contracts.Enums.NatureGroup)groupViewModel.NatureId,
            });

            return await TreatResponseAsync(response, "Index");
        }

        public async Task<IActionResult> DeleteAsync(short id)
        {
            await _GroupClient.DeleteAsync(id);
            TempData["Message"] = "Group delete successfully!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var natures = await _GroupClient.GetNatures();

            ViewBag.Natures = natures.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });

            var response = await _GroupClient.GetAsync(id);

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> NewAsync()
        {
            var natures = await _GroupClient.GetNatures();

            ViewBag.Natures = natures.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });

            return View();
        }
    }
}
