using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Finan.Web.Clients
{
    public class ClassificationClient : BaseClient, IClassificationClient
    {
        public ClassificationClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }
        const string _baseUrl = "Classification";
        public async Task<ApiResponse<List<ClassificationResponse>>> GetAllAsync() => await GetAsync<List<ClassificationResponse>>(_baseUrl);
        public async Task<ApiResponse<ClassificationResponse>> GetAsync(int id) => await GetAsync<ClassificationResponse>(_baseUrl + "/" + id);
        public async Task<ApiResponse<ClassificationResponse>> CreateAsync(ClassificationRequest Classification) => await RequestAsync<ClassificationResponse>(_baseUrl, "", Classification, RestSharp.Method.Post);
        public async Task<ApiResponse<ClassificationResponse>> UpdateAsync(ClassificationRequest Classification) => await RequestAsync<ClassificationResponse>(_baseUrl, "", Classification, RestSharp.Method.Put);
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");
        public async Task<ApiResponse<PagedResponse<ClassificationResponse>?>> GetPageAsync(int pageNumber, int pagesize) => await GetAsync<PagedResponse<ClassificationResponse>>(_baseUrl + "/" + "Paged" + "/" + pageNumber + "/" + pagesize);
        public async Task<ApiResponse<List<FinancialTypeResponse>>> GetFinancialTypeList() => await GetAsync<List<FinancialTypeResponse>>(_baseUrl + "/" + "GetClassificationTypeList");
        public async Task<ApiResponse<List<ClassificationResponse>>> GetClassificationsFromReceivableByGroupIdAsync(int GroupId) => await GetAsync<List<ClassificationResponse>>(_baseUrl + "/" + "GetClassificationsFromReceivableByGroupIdAsync" + "/" + GroupId);
        public async Task<ApiResponse<List<ClassificationResponse>>> GetClassificationsFromTransactionByGroupIdAsync(int GroupId) => await GetAsync<List<ClassificationResponse>>(_baseUrl + "/" + "Group" + "/" + GroupId);
    }
}
