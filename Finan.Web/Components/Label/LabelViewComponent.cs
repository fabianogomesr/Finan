using Microsoft.AspNetCore.Mvc;

namespace Finan.Web.Components.Label
{
    public class LabelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name, string? text = default)
        {
            ViewBag.Name = name;
            ViewBag.Text = text;
            return View();
        }
    }
}
