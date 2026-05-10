using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IBankRepository : IBaseRepository<Bank>
    {
        Task<PagedResult<BankResponse>> GetBanksAsync(int pageNumber = 1, int pageSize = 5);
    }
}
