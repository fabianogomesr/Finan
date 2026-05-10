using Finan.Contracts.Filters;
using Finan.Contracts.Request;
using Finan.Contracts.Response;

namespace Finan.Web.Clients
{
    public interface ITransactionClient
    {
        Task<ApiResponse<List<TransactionResponse>>?> GetAllAsync();
        Task<ApiResponse<PagedResponse<TransactionResponse>>?> GetPageAsync(TransactionFilter filter);
        Task<ApiResponse<TransactionResponse>> GetAsync(int id);
        Task<ApiResponse<TransactionResponse>> CreateAsync(TransactionRequest Transaction);
        Task<ApiResponse<TransactionResponse>> UpdateAsync(TransactionRequest Transaction);
        Task DeleteAsync(int id);
        Task<ApiResponse<List<FinancialTypeResponse>>> GetFinancialTypeList();
        Task<ApiResponse<List<TransactionStatusResponse>>> GetStatusList();
        Task<ApiResponse<List<DateTypeResponse>>?> GetDateTypeList();
    }
}
