using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Finan.Web.Clients
{
    public class BankClient : BaseClient, IBankClient
    {
        public BankClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }
        const string _baseUrl = "Bank";
        public async Task<ApiResponse<List<BankResponse>>> GetAllAsync() => (await GetAsync<List<BankResponse>>(_baseUrl));
        public async Task<ApiResponse<BankResponse>> GetAsync(int id) => (await GetAsync<BankResponse>(_baseUrl + "/" + id));
        public async Task<ApiResponse<BankResponse>> CreateAsync(BankRequest Bank) => (await RequestAsync<BankResponse>(_baseUrl, "", Bank, RestSharp.Method.Post));
        public async Task<ApiResponse<BankResponse>> UpdateAsync(BankRequest Bank) => (await RequestAsync<BankResponse>(_baseUrl, "", Bank, RestSharp.Method.Put));
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");

        public async Task<ApiResponse<PagedResponse<BankResponse>?>> GetPageAsync(int pageNumber, int pagesize) => (await GetAsync<PagedResponse<BankResponse>>(_baseUrl + "/"  + "Paged" + "/" + pageNumber + "/" + pagesize));

    }
}
