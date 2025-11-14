using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IBankService : IBaseService
    {
        Task<BankDTO?> CreateAsync(BankCommand bankCommand);
        Task<BankDTO?> UpdateAsync(BankCommand bankCommand);
        Task<BankDTO?> GetByIdAsync(int id);
        Task<List<BankDTO?>> GetAsync();
        Task<PagedResult<BankDTO>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);
    }
}
