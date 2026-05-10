using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICostCenterRepository : IBaseRepository<CostCenter>
    {
        Task<PagedResult<CostCenterResponse>> GetBanksAsync(int pageNumber = 1, int pageSize = 5);
    }
}
