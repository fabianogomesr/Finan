using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Finan.Web.Components.DecimalInput
{
    public class DecimalInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ModelExplorer model, string name, decimal? value = null, string placeholder = "Digite um valor", string step = "0.01")
        {
            ViewBag.Name = name;
            ViewBag.Value = value;
            ViewBag.Placeholder = placeholder;
            ViewBag.Step = step;
            return View(model);
        }
    }
}
