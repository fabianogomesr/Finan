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
    public class CostCenterClient : BaseClient, ICostCenterClient
    {
        public CostCenterClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }
        const string _baseUrl = "CostCenter";
        public async Task<ApiResponse<List<CostCenterResponse>>> GetAllAsync() => await GetAsync<List<CostCenterResponse>>(_baseUrl);
        public async Task<ApiResponse<CostCenterResponse>> GetAsync(int id) => await GetAsync<CostCenterResponse>(_baseUrl + "/" + id);
        public async Task<ApiResponse<CostCenterResponse>> CreateAsync(CostCenterRequest CostCenter) => await RequestAsync<CostCenterResponse>(_baseUrl, "", CostCenter, RestSharp.Method.Post);
        public async Task<ApiResponse<CostCenterResponse>> UpdateAsync(CostCenterRequest CostCenter) => await RequestAsync<CostCenterResponse>(_baseUrl, "", CostCenter, RestSharp.Method.Put);
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");
        public async Task<ApiResponse<PagedResponse<CostCenterResponse>>> GetPageAsync(int pageNumber, int pagesize) => await GetAsync<PagedResponse<CostCenterResponse>>(_baseUrl + "/" + "Paged" + "/" + pageNumber + "/" + pagesize);

    }
}
