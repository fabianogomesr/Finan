using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface ICostCenterService : IBaseService
    {
        Task<CostCenterDTO?> CreateAsync(CostCenterCommand costCenterCommand);
        Task<CostCenterDTO?> UpdateAsync(CostCenterCommand costCenterCommand);
        Task<CostCenterDTO?> GetByIdAsync(int id);
        Task<List<CostCenterDTO>?> GetAsync();
        Task<PagedResult<CostCenterDTO>?> GetAsync(int pageNumber = 1, int pageSize = 5);
        Task DeleteAsync(int id);
    }
}
