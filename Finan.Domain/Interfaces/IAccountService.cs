using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IAccountService : IBaseService
    {
        Task<AccountDTO?> CreateAsync(AccountCommand AccountParameter);
        Task<AccountDTO?> UpdateAsync(AccountCommand AccountParameter);
        Task<AccountDTO?> GetByIdAsync(int id);
        Task<List<AccountDTO?>> GetAsync();
        Task<PagedResult<AccountDTO>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);

    }
}
