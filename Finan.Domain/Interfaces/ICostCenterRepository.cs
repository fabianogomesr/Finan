using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICostCenterRepository : IBaseRepository<CostCenter>
    {
        Task<PagedResult<CostCenterDTO>> GetBanksAsync(int pageNumber = 1, int pageSize = 5);
    }
}
