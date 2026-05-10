using Finan.Contracts.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<IActionResult> HandleUnauthorizedAsync()
        {
            // Realiza o logout, removendo o cookie de autenticação
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redireciona para a página de login após o logout
            return RedirectToAction("Login", "Auth");
        }

        protected async Task<IActionResult> TreatResponseAsync<T>(ApiResponse<T> response)
        {
            if (response.Unauthorized)
                return await HandleUnauthorizedAsync();

            if (!response.Success)
            {
                TempData["ModalTitle"] = "Erro ao processar a requisição";
                TempData["ModalType"] = "danger";
                TempData["ModalMessages"] = response.Messages.ToList();
                return RedirectToAction("Index");
            }

            ViewBag.Data = response.Data;

            return View(); // Indica que não houve erro
        }

        protected async Task<IActionResult> TreatResponseAsync<T>(ApiResponse<T> response, string actionName)
        {
            if (response.Unauthorized)
            {
                return await HandleUnauthorizedAsync();
            }

            if (!response.Success)
            {
                TempData["ModalTitle"] = "Erro ao processar a requisição";
                TempData["ModalType"] = "danger";
                TempData["ModalMsessages"] = response.Messages.ToList();
                return RedirectToAction("Index");
            }

            ViewBag.Data = response.Data;

            TempData["Message"] = "Operação realizada com sucesso!";

            return RedirectToAction(actionName);
        }

        protected IActionResult ValidateModel(string actionName)
        {
            TempData["ModalTitle"] = "Erro ao processar a requisição";
            TempData["ModalType"] = "danger";
            TempData["ModalMessages"] = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return RedirectToAction(actionName);
        }
    }
}
