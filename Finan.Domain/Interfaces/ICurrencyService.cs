using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICurrencyService : IBaseService
    {
        Task<CurrencyDTO?> CreateAsync(CurrencyCommand bankCommand);
        Task<CurrencyDTO?> UpdateAsync(CurrencyCommand bankCommand);
        Task<CurrencyDTO?> GetByIdAsync(int id);
        Task<List<CurrencyDTO>?> GetAsync();
        Task<PagedResult<CurrencyDTO>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);
    }
}
