using Finan.Contracts.Request;
using Finan.Contracts.Response;

namespace Finan.Web.Clients
{
    public interface IAccountClient
    {
        Task<ApiResponse<List<AccountResponse>>> GetAllAsync();
        Task<ApiResponse<PagedResponse<AccountResponse>>> GetPageAsync(int pageNumber = 1, int pagesize = 5);
        Task<ApiResponse<AccountResponse>> GetAsync(int id);
        Task<ApiResponse<AccountResponse>> CreateAsync(AccountRequest Account);
        Task<ApiResponse<AccountResponse>> UpdateAsync(AccountRequest Account);
        Task DeleteAsync(int id);
    }
}
