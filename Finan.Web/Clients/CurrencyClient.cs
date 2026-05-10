using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Web.Clients
{
    public class CurrencyClient : BaseClient, ICurrencyClient
    {
        public CurrencyClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }
        const string _baseUrl = "Currency";
        public async Task<ApiResponse<List<CurrencyResponse>>> GetAllAsync() => await GetAsync<List<CurrencyResponse>>(_baseUrl);
        public async Task<ApiResponse<CurrencyResponse>> GetAsync(int id) => await GetAsync<CurrencyResponse>(_baseUrl + "/" + id);
        public async Task<ApiResponse<CurrencyResponse>> CreateAsync(CurrencyRequest Currency) => await RequestAsync<CurrencyResponse>(_baseUrl, "", Currency, RestSharp.Method.Post);
        public async Task<ApiResponse<CurrencyResponse>> UpdateAsync(CurrencyRequest Currency) => await RequestAsync<CurrencyResponse>(_baseUrl, "", Currency, RestSharp.Method.Put);
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");
        public async Task<ApiResponse<PagedResponse<CurrencyResponse>?>> GetPageAsync(int pageNumber, int pagesize) => await GetAsync<PagedResponse<CurrencyResponse>>(_baseUrl + "/" + "Paged" + "/" + pageNumber + "/" + pagesize);

    }
}
