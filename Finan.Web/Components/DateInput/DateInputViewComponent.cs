using Microsoft.AspNetCore.Mvc;

namespace Finan.Web.Components.DateInput
{
    public class DateInputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string name, DateTime? value = null, string placeholder = "Selecione uma data")
        {
            ViewBag.Name = name;
            ViewBag.Value = value.HasValue ? value.Value.ToString("yyyy-MM-dd") : string.Empty; // Formato padrão para inputs de data
            ViewBag.Placeholder = placeholder;
            return View();
        }
    }
}
