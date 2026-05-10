
using Finan.Contracts.Request;
using Finan.Contracts.Response;

namespace Finan.Web.Clients
{
    public interface IBankClient
    {
        Task<ApiResponse<List<BankResponse>>> GetAllAsync();
        Task<ApiResponse<PagedResponse<BankResponse>>> GetPageAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<BankResponse>> GetAsync(int id);
        Task<ApiResponse<BankResponse>> CreateAsync(BankRequest Bank);
        Task<ApiResponse<BankResponse>> UpdateAsync(BankRequest Bank);
        Task DeleteAsync(int id);
    }
}
