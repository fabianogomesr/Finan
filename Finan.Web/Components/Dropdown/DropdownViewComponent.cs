using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Finan.Web.Components.Dropdown
{
    public class DropdownViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name, IEnumerable<SelectListItem>? items = default, string? selectedValue = null, bool required = true)
        {
            ViewBag.Name = name;
            ViewBag.Items = items;
            ViewBag.SelectedValue = selectedValue;
            ViewBag.Required = required;
            return View();
        }
    }
}
