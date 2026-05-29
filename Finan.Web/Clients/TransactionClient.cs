using Finan.Contracts.Filters;
using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Web.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Finan.Web.Clients
{
    public class TransactionClient : BaseClient, ITransactionClient
    {
        public TransactionClient(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }

        const string _baseUrl = "Transaction";
        public async Task<ApiResponse<List<TransactionResponse>>?> GetAllAsync() 
        {
            var response = await GetAsync<List<TransactionResponse>>(_baseUrl);
            return response;
        }
        public async Task<ApiResponse<TransactionResponse>?> GetAsync(int id) 
        {
            var response = await GetAsync<TransactionResponse>(_baseUrl + "/" + id);
            return response;
        }
        public async Task<ApiResponse<TransactionResponse>?> CreateAsync(TransactionRequest TransactionRequest) 
        {
            var response = await RequestAsync<TransactionResponse>(_baseUrl, "", TransactionRequest, RestSharp.Method.Post);
            return response;
        } 
        public async Task<ApiResponse<TransactionResponse>?> UpdateAsync(TransactionRequest TransactionRequest) 
        {
            var response = await RequestAsync<TransactionResponse>(_baseUrl, "", TransactionRequest, RestSharp.Method.Put);
            return response;
        } 
        public async Task DeleteAsync(int id) => await DeleteAsync(_baseUrl + "/" + id, "");
        public async Task<ApiResponse<PagedResponse<TransactionResponse>>?> GetPageAsync(TransactionFilter filter) 
        {
            var response = await GetAsync<PagedResponse<TransactionResponse>>($"{_baseUrl}/Filtered?TransactionType={filter.TransactionType.GetHashCode()}&DateType={filter.DateType}&StartDate={filter.StartDate.ToString("yyyy/MM/dd")}&EndDate={filter.EndDate.ToString("yyyy/MM/dd")}&PageNumber={filter.PageNumber}&PageSize={filter.PageSize}&Canceled={filter.Canceled}");
            return response;
        }
        public async Task<ApiResponse<List<FinancialTypeResponse>>?> GetFinancialTypeList() 
        {
            var response = await GetAsync<List<FinancialTypeResponse>>(_baseUrl + "/" + "Types");
            return response;
        } 
        public async Task<ApiResponse<List<TransactionStatusResponse>>?> GetStatusList() 
        {
            var response = await GetAsync<List<TransactionStatusResponse>>(_baseUrl + "/" + "Status");
            return response;
        } 
        public async Task<ApiResponse<List<DateTypeResponse>>?> GetDateTypeList() 
        {
            var response = await GetAsync<List<DateTypeResponse>>(_baseUrl + "/" + "DateTypes");
            return response;
        } 
    }
}
