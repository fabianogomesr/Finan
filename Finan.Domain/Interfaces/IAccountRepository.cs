using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account> GetAccountByIdAsync(int id);
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<PagedResult<AccountDTO>> GetAccountsAsync(int pageNumber = 1, int pageSize = 5);
    }
}
