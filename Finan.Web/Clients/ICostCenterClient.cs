using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.Contracts.Request;
using Finan.Contracts.Response;

namespace Finan.Web.Clients
{
    public interface ICostCenterClient
    {
        Task<ApiResponse<List<CostCenterResponse>>> GetAllAsync();
        Task<ApiResponse<PagedResponse<CostCenterResponse>>> GetPageAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<CostCenterResponse>> GetAsync(int id);
        Task<ApiResponse<CostCenterResponse>> CreateAsync(CostCenterRequest CostCenter);
        Task<ApiResponse<CostCenterResponse>> UpdateAsync(CostCenterRequest CostCenter);
        Task DeleteAsync(int id);
    }
}
