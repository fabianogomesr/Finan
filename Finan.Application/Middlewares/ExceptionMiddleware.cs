using Finan.Application.Common;
using System.Net;
using System.Text.Json;

namespace Finan.Application.Middlewares
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ApiResponse(messages: new[] { "Ocorreu um erro interno no servidor: " + ex.Message }, success:false);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
