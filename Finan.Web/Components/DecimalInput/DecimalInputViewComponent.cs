using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Finan.Web.Components.DecimalInput
{
    public class DecimalInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ModelExplorer model, string name, decimal? value = null, string placeholder = "Digite um valor", string step = "0.01", bool required = true)
        {
            ViewBag.Name = name;
            ViewBag.Value = value;
            ViewBag.Placeholder = placeholder;
            ViewBag.Step = step;
            ViewBag.Required = required;
            return View(model);
        }
    }
}
