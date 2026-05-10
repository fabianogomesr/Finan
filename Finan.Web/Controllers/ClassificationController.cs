using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finan.Web.Controllers
{
    [Authorize]
    public class ClassificationController : BaseController
    {
        private IClassificationClient _classificationClient;
        private IGroupClient _group;

        public ClassificationController(IClassificationClient classificationClient, IGroupClient group, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _classificationClient = classificationClient;
            _group = group;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await _classificationClient.GetPageAsync();
            return await TreatResponseAsync(response);
        }

        [HttpGet("Classification/Page/{pageNumber}")]
        public async Task<IActionResult> IndexAsync(int pageNumber = 1)
        {
            var response = await _classificationClient.GetPageAsync(pageNumber);

            if (response.Data.Page > 1 && !response.Data.Items.Any())
                return RedirectToAction("Index", new { pageNumber = 1 });

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> UpdateAsync(ClassificationViewModel classificationViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("Edit");

            var classification = new ClassificationRequest
            {
                Id = classificationViewModel.Id,
                Description = classificationViewModel.Description,
                GroupId = classificationViewModel.GroupId
            };

            var response = await _classificationClient.UpdateAsync(classification);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task<IActionResult> CreateAsync(ClassificationViewModel classificationViewModel)
        {
            if (!ModelState.IsValid)
                return ValidateModel("New");

            var classification = new ClassificationRequest
            {
                Id = 0,
                Description = classificationViewModel.Description,
                GroupId = classificationViewModel.GroupId,
            };

            var response = await _classificationClient.CreateAsync(classification);

            return await TreatResponseAsync(response, "Index");
        }

        public async Task DeleteAsync(short id)
        {
            await _classificationClient.DeleteAsync(id);
            TempData["Message"] = "Classification delete successfully!";
            Response.Redirect("/Classification/Page/1");
        }

        public async Task<IActionResult> EditAsync(short id)
        {
            var groups = await _group.GetAllAsync();

            ViewBag.Groups = groups.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });

            var response = await _classificationClient.GetAsync(id);

            return await TreatResponseAsync(response);
        }

        public async Task<IActionResult> NewAsync() 
        {
            var groups = await _group.GetAllAsync();

            ViewBag.Groups = groups.Data?.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Description });

            return View();
        } 


    }
}
