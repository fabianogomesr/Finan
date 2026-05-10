using System.Net;
using System.Text.Json;
using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Finan.Web.Clients
{
    public class BaseClient : RestClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _baseUrl;

        public BaseClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _baseUrl = configuration.GetSection("Api:BaseUrl").Value ?? string.Empty;
        }

        private RestClient CreateClient(string? url)
        {
            var baseUri = _baseUrl + url;
            return new RestClient(baseUri);
        }

        private string? GetToken()
        {
            return _httpContextAccessor?.HttpContext?.Request?.Cookies["jwt"];
        }

        private RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Accept", "application/json");
            var token = GetToken();
            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        private ApiResponse<T> BuildErrorResponse<T>(string message)
            where T : new()
        {
            return new ApiResponse<T>
            {
                Success = false,
                Messages = new List<string> { message },
                Unauthorized = false
            };
        }

        private ApiResponse<T> TryDeserializeFallback<T>(RestResponse response) where T : new()
        {
            if (string.IsNullOrEmpty(response.Content))
                return new ApiResponse<T>();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var parsed = JsonSerializer.Deserialize<ApiResponse<T>>(response.Content, options);
                return parsed ?? new ApiResponse<T>();
            }
            catch
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    Messages = new List<string> { "Resposta inválida do servidor." }
                };
            }
        }

        public async Task<ApiResponse<T>?> GetAsync<T>(string url) where T : new()
        {
            var client = CreateClient(url);
            var request = CreateRequest(string.Empty, Method.Get);

            var response = await client.ExecuteAsync<ApiResponse<T>>(request).ConfigureAwait(false);

            if (response == null)
                return BuildErrorResponse<T>("Sem resposta do servidor.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ApiResponse<T>
                {
                    Unauthorized = true,
                    Success = false,
                    Messages = new List<string> { "Usuário desautenticado (Unauthorized). Sessão limpa." }
                };
            }

            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.NoContent)
                return new ApiResponse<T>();

            if (!response.IsSuccessful)
            {
                // tentar extrair mensagens do body mesmo que response.Data esteja null
                var errors = response.Data?.Messages != null && response.Data.Messages.Any()
                    ? string.Join(", ", response.Data.Messages)
                    : null;

                if (string.IsNullOrEmpty(errors) && !string.IsNullOrEmpty(response.Content))
                {
                    var fallback = TryDeserializeFallback<T>(response);
                    errors = fallback.Messages != null && fallback.Messages.Any()
                        ? string.Join(", ", fallback.Messages)
                        : errors;
                }

                return new ApiResponse<T>
                {
                    Success = false,
                    Messages = new List<string> { "Erro ao realizar GET: " + (string.IsNullOrEmpty(errors) ? response.ErrorMessage ?? "Erro Desconhecido" : errors) }
                };
            }

            // Se deserialização falhou, tentar fallback
            if (response.Data == null)
                return TryDeserializeFallback<T>(response);

            return response.Data;
        }

        public async Task DeleteAsync(string url, string resource)
        {
            var client = CreateClient(url);
            var request = CreateRequest(resource, Method.Delete);

            var response = await client.ExecuteAsync(request).ConfigureAwait(false);

            if (response == null)
                throw new HttpRequestException("Sem resposta do servidor ao tentar deletar.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new HttpRequestException("Usuário desautenticado (Unauthorized).");

            if (!response.IsSuccessful)
            {
                var detail = !string.IsNullOrEmpty(response.Content) ? response.Content : response.ErrorMessage ?? "Erro Desconhecido";
                throw new HttpRequestException($"Erro ao realizar DELETE: {detail}");
            }
        }

        public async Task<ApiResponse<T>?> RequestAsync<T>(string url, string resource, object data, Method method) where T : new()
        {
            var client = CreateClient(url);
            var request = CreateRequest(resource, method);

            if (method != Method.Get && data != null)
                request.AddJsonBody(data);

            var response = await client.ExecuteAsync<ApiResponse<T>>(request).ConfigureAwait(false);

            if (response == null)
                return BuildErrorResponse<T>("Sem resposta do servidor.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ApiResponse<T>
                {
                    Unauthorized = true,
                    Success = false,
                    Messages = new List<string> { "Usuário desautenticado (Unauthorized). Sessão limpa." }
                };
            }

            if (!response.IsSuccessful)
            {
                var errors = response.Data?.Messages != null && response.Data.Messages.Any()
                    ? string.Join(", ", response.Data.Messages)
                    : null;

                if (string.IsNullOrEmpty(errors) && !string.IsNullOrEmpty(response.Content))
                {
                    var fallback = TryDeserializeFallback<T>(response);
                    errors = fallback.Messages != null && fallback.Messages.Any()
                        ? string.Join(", ", fallback.Messages)
                        : errors;
                }

                return new ApiResponse<T>
                {
                    Success = false,
                    Messages = new List<string> { $"Erro ao realizar {method}: " + (string.IsNullOrEmpty(errors) ? response.ErrorMessage ?? "Erro Desconhecido" : errors) }
                };
            }

            if (response.Data == null)
                return TryDeserializeFallback<T>(response);

            return response.Data;
        }
    }
}
