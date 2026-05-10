using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Finan.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continua o pipeline normalmente
                await _next(context);
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção não tratada
                _logger.LogError(ex, "Erro não tratado capturado pelo middleware.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static bool IsApiOrAjaxRequest(HttpRequest request)
        {
            var accept = request.Headers["Accept"].ToString();
            var xRequested = request.Headers["X-Requested-With"].ToString();
            var path = request.Path.ToString();

            if (!string.IsNullOrEmpty(xRequested) && xRequested.Equals("XMLHttpRequest", StringComparison.OrdinalIgnoreCase))
                return true;

            if (!string.IsNullOrEmpty(accept) && (accept.Contains("application/json") || accept.Contains("text/json")))
                return true;

            if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Se for uma requisição que espera HTML (navegador normal), retornamos uma página minimal com um modal JS
            var accept = context.Request.Headers["Accept"].ToString();
            var wantsHtml = !IsApiOrAjaxRequest(context.Request) && accept.Contains("text/html", StringComparison.OrdinalIgnoreCase);

            if (wantsHtml)
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                var encodedMessage = JsonSerializer.Serialize(message);
                var html = $@"<!doctype html>
                    <html>
                      <head>
                        <meta charset='utf-8'>
                        <meta name='viewport' content='width=device-width,initial-scale=1'>
                        <title>Erro</title>
                        <style>
                          /* Estilo azul do sistema */
                          .fw-overlay {{ position:fixed; inset:0; background:rgba(0,0,0,0.45); display:flex; align-items:center; justify-content:center; z-index:2147483647; }}
                          .fw-box {{
                            width: min(720px, 92%);
                            background: linear-gradient(180deg, #f0f7ff 0%, #e6f3ff 100%);
                            border: 1px solid rgba(11,99,214,0.15);
                            box-shadow: 0 8px 30px rgba(11,99,214,0.12);
                            border-radius: 10px;
                            padding: 20px;
                            color: #023a77;
                            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial;
                          }}
                          .fw-title {{ font-size:18px; font-weight:600; margin:0 0 8px 0; color:#0b63d6; }}
                          .fw-message {{ margin:0 0 16px 0; line-height:1.4; color:#134a8a; }}
                          .fw-actions {{ display:flex; gap:8px; justify-content:flex-end; }}
                          .fw-btn {{
                            background:#0b63d6; color:#fff; border:none; padding:8px 14px; border-radius:6px; cursor:pointer;
                            font-weight:600; box-shadow: 0 2px 8px rgba(11,99,214,0.15);
                          }}
                          .fw-btn-secondary {{
                            background:transparent; color:#0b63d6; border:1px solid rgba(11,99,214,0.12);
                          }}
                          @media (max-width:420px) {{ .fw-box {{ padding:16px; }} .fw-title{{font-size:16px}} }}
                        </style>
                      </head>
                      <body>
                        <script>
                          (function() {{
                            try {{
                              var msg = {encodedMessage};
                              var overlay = document.createElement('div'); overlay.className = 'fw-overlay';
                              var box = document.createElement('div'); box.className = 'fw-box';
                              var title = document.createElement('div'); title.className = 'fw-title'; title.textContent = 'Erro';
                              var p = document.createElement('p'); p.className = 'fw-message'; p.textContent = msg;
                              var actions = document.createElement('div'); actions.className = 'fw-actions';
                              var btnClose = document.createElement('button'); btnClose.className = 'fw-btn'; btnClose.textContent = 'Fechar';
                              var btnHome = document.createElement('button'); btnHome.className = 'fw-btn fw-btn-secondary'; btnHome.textContent = 'Ir para a tela principal';

                              // Fechar: redireciona para a página principal do sistema
                              btnClose.onclick = function() {{ window.location.href = '/'; }};
                              // Alternativa: permitir apenas fechar sem redirecionar (se desejar)
                              btnHome.onclick = function() {{ window.location.href = '/'; }};

                              actions.appendChild(btnHome);
                              actions.appendChild(btnClose);

                              box.appendChild(title);
                              box.appendChild(p);
                              box.appendChild(actions);
                              overlay.appendChild(box);
                              document.body.appendChild(overlay);

                              // Foco no botão para acessibilidade
                              btnClose.focus();

                              // Se o usuário clicar fora do box, também redireciona para a tela principal
                              overlay.addEventListener('click', function(e) {{
                                if (e.target === overlay) {{
                                  window.location.href = '/';
                                }}
                              }});
                            }} catch (e) {{
                              // Caso algo falhe no script, garantir redirecionamento simples
                              try {{ window.location.href = '/'; }} catch (__) {{ }}
                            }}
                          }})();
                        </script>
                      </body>
                    </html>";
                await context.Response.WriteAsync(html);
                return;
            }

            // Para APIs/AJAX, retorna JSON com flag para o cliente exibir modal e opção de redirecionamento
            context.Response.ContentType = "application/json";
            var response = new
            {
                Success = false,
                ShowModal = true,
                Messages = new[] { message },
                RedirectTo = "/" // cliente pode usar essa informação para redirecionar à tela principal
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
