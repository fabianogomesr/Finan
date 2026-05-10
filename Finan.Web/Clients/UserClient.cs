using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Finan.Web.Clients
{
    public class UserClient : BaseClient, IUserClient
    {
        private readonly string _baseAuthUrl;
        private readonly string _baseUrl;

        public UserClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
            var baseUrl = configuration.GetSection("Api:BaseUrl").Value ?? string.Empty;
            _baseAuthUrl = baseUrl + "Auth/Login";
            _baseUrl = baseUrl + "User";
        }

        public async Task<string> GetTokenAsync(string userName, string pass) 
        {
            try
            { 
                var client = new RestClient(_baseAuthUrl);

                var request = new RestRequest("", method: Method.Post);

                request.AddJsonBody(new
                {
                    UserName = userName,
                    Password = pass
                }); // Envia os dados como JSON


                var response = await client.ExecuteAsync<ApiResponse<string>>(request);

                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return null;
                }

                if (!response.IsSuccessful)
                {
                    throw new Exception($"Erro ao realizar login: {response.ErrorMessage}");
                }

                return response.Data?.Data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserResponse> CreateAdminUserAsync(UserRequest user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Usuário não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(user.UserName) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("Dados obrigatórios do usuário não informados.");
            }

            var client = new RestClient(_baseUrl);
            var request = new RestRequest("", Method.Post);

            request.AddJsonBody(new
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = "Admin"
            });

            var response = await client.ExecuteAsync<UserResponse>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Acesso não autorizado ao criar usuário admin.");
            }

            if (!response.IsSuccessful)
            {
                var errorMsg = !string.IsNullOrEmpty(response.ErrorMessage)
                    ? response.ErrorMessage
                    : response.StatusDescription;
                throw new Exception($"Erro ao criar usuário admin: {errorMsg}");
            }

            if (response.Data == null)
            {
                throw new Exception("Resposta inesperada: usuário admin não foi retornado.");
            }

            return response.Data;
        }
    }
}
