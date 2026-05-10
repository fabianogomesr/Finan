using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICurrencyRepository : IBaseRepository<Currency>
    {
        Task<PagedResult<CurrencyResponse>> GetBanksAsync(int pageNumber = 1, int pageSize = 5);
    }
}
