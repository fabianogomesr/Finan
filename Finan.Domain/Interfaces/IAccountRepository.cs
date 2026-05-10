using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> GetAccountByIdAsync(int id);
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<PagedResult<AccountResponse>> GetAccountsAsync(int pageNumber = 1, int pageSize = 5);
    }
}
