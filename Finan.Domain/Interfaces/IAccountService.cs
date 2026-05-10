using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IAccountService : IBaseService
    {
        Task<AccountResponse?> CreateAsync(AccountRequest AccountParameter);
        Task<AccountResponse?> UpdateAsync(AccountRequest AccountParameter);
        Task<AccountResponse?> GetByIdAsync(int id);
        Task<List<AccountResponse?>> GetAsync();
        Task<PagedResult<AccountResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);

    }
}
