using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICurrencyRepository : IBaseRepository<Currency>
    {
        Task<PagedResult<CurrencyDTO>> GetBanksAsync(int pageNumber = 1, int pageSize = 5);
    }
}
