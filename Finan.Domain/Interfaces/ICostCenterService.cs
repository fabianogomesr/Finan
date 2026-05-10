using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICostCenterService : IBaseService
    {
        Task<CostCenterResponse?> CreateAsync(CostCenterRequest costCenterCommand);
        Task<CostCenterResponse?> UpdateAsync(CostCenterRequest costCenterCommand);
        Task<CostCenterResponse?> GetByIdAsync(int id);
        Task<List<CostCenterResponse>?> GetAsync();
        Task<PagedResult<CostCenterResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);
    }
}
