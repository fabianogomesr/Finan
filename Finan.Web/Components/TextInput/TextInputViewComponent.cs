using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Finan.Web.Components.TextInput
{
    public class TextInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ModelExplorer model, string name, int maxlength, string? value = null, string placeholder = "Digite um valor", bool upperCase = false, bool required = true)
        {

            ViewBag.Name = name;
            ViewBag.Value = value;
            ViewBag.Placeholder = placeholder;
            ViewBag.Maxlength = maxlength;
            ViewBag.UpperCase = upperCase;
            ViewBag.Required = required;
            return View(model);
        }
    }
}
