using Finan.Contracts.Request;
using Finan.Web.Clients;
using Finan.Web.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class AuthController : BaseController
{
    private readonly IUserClient _userClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(IUserClient userClient, IHttpContextAccessor httpContextAccessor) :  base(httpContextAccessor)
    {
        _userClient = userClient;
        _httpContextAccessor = httpContextAccessor;
    }
    // Exibe a página de login
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    // Exibe a página de registro
    [HttpGet]
    [AllowAnonymous]
    public IActionResult RegisterAsync()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    // Processa o login
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Aqui você pode colocar a lógica de autenticação
            // Por exemplo: verificar se o usuário e senha estão corretos

            var token = await _userClient.GetTokenAsync(model.Username, model.Password);

            if (token == null)
            {
                ModelState.AddModelError("", "Login inválido.");
                return View();
            }

            // Decodifica o token JWT para extrair as claims
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            var expiration = jsonToken?.ValidTo;

            if (expiration <= DateTime.UtcNow)
            {
                // O token já está expirado, forçar logout
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            var claims = new List<Claim>();

            // Adiciona as claims do token JWT
            claims.AddRange(jsonToken.Claims);

            // Adiciona uma claim de nome (caso não exista)
            if (!claims.Any(c => c.Type == ClaimTypes.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, model.Username));
            }

            // Cria o ClaimsIdentity e ClaimsPrincipal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            AuthenticationProperties authProperties;

            if (model.RememberMe)
            {
                // Define o tempo de expiração do cookie para 30 dias se "Lembrar-me" estiver marcado
                authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30),
                    RedirectUri = "/Home/Index" // Redireciona para a página inicial após o login
                };
            }
            else
            {
                // Define o tempo de expiração do cookie para 30 minutos se "Lembrar-me" não estiver marcado
                authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                    RedirectUri = "/Home/Index" // Redireciona para a página inicial após o login
                };
            }

            // Autentica o usuário e armazena o token JWT como um cookie de autenticação
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            //// Armazena o token JWT em um cookie separado para futuras requisições
            HttpContext.Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            // Redireciona para a página inicial ou para onde desejar
            return RedirectToAction("Index", "Home");

        }

        return View(model);
    }

    //// Processa o registro
    //[HttpPost]
    //[AllowAnonymous]
    //public async Task<IActionResult> Register(RegisterViewModel model)
    //{
    //    if (User.Identity.IsAuthenticated)
    //    {
    //        return RedirectToAction("Index", "Home");
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        // Aqui você pode colocar a lógica de registro
    //        // Por exemplo: criar um novo usuário no banco de dados

    //        var user = new UserRequest
    //        {
    //            UserName = model.Name,
    //            Email = model.Email,
    //            Password = model.Password
    //        };

    //        var result = await _userClient.CreateAdminUserAsync(user);

    //        if (result !=  null)
    //        {
    //            // Redireciona para a página de login após o registro bem-sucedido
    //            TempData["Message"] = "Usuário cadastrado!";
    //            return RedirectToAction("Login", "Auth");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError("", "Erro ao registrar usuário.");
    //        }
    //    }
    //    return View(model);
    //}

    // Método para realizar o logout
    [HttpGet]
    public async Task<IActionResult> Logout() => await HandleUnauthorizedAsync();
}