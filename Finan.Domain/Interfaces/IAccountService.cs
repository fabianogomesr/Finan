using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IAccountService : IBaseService<Account>
    {
        Task<AccountDTO> AddAccount<AccountValidator>(AccountCommand AccountParameter);
        Task<AccountDTO> GetAccountByIdAsync(int id);
        Task<IEnumerable<AccountDTO>> GetAccountsAsync();
        Task<AccountPaginationDTO> GetAccountsAsync(int pageNumber = 1, int pageSize = 5);
        Task<AccountDTO> UpdateAccount<AccountValidator>(AccountCommand AccountParameter);
    }
}
