using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Finan.Web.Clients
{
    public class AccountClient : BaseClient, IAccountClient
    {
        public AccountClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }
        const string _baseUrl = "Account";
        public async Task<ApiResponse<List<AccountResponse>>> GetAllAsync() => (await GetAsync<List<AccountResponse>>(_baseUrl));
        public async Task<ApiResponse<AccountResponse>> GetAsync(int id) => (await GetAsync<AccountResponse>(_baseUrl + "/" + id));
        public async Task<ApiResponse<AccountResponse>> CreateAsync(AccountRequest Account) => (await RequestAsync<AccountResponse>(_baseUrl, "", Account, RestSharp.Method.Post));
        public async Task<ApiResponse<AccountResponse>> UpdateAsync(AccountRequest Account) => (await RequestAsync<AccountResponse>(_baseUrl, "", Account, RestSharp.Method.Put));
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");
        public async Task<ApiResponse<PagedResponse<AccountResponse>?>> GetPageAsync(int pageNumber = 1, int pagesize = 5) => await GetAsync<PagedResponse<AccountResponse>>(_baseUrl + "/" + "Paged" + "/" + pageNumber + "/" + pagesize);
    }
}
