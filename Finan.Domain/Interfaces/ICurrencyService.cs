using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICurrencyService : IBaseService
    {
        Task<CurrencyResponse?> CreateAsync(CurrencyRequest bankCommand);
        Task<CurrencyResponse?> UpdateAsync(CurrencyRequest bankCommand);
        Task<CurrencyResponse?> GetByIdAsync(int id);
        Task<List<CurrencyResponse>?> GetAsync();
        Task<PagedResult<CurrencyResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);
    }
}
