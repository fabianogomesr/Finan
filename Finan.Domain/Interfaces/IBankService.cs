using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IBankService : IBaseService
    {
        Task<BankResponse?> CreateAsync(BankRequest bankCommand);
        Task<BankResponse?> UpdateAsync(BankRequest bankCommand);
        Task<BankResponse?> GetByIdAsync(int id);
        Task<List<BankResponse?>> GetAsync();
        Task<PagedResult<BankResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);
    }
}
