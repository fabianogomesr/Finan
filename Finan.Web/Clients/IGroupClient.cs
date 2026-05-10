
using Finan.Contracts.Request;
using Finan.Contracts.Response;

namespace Finan.Web.Clients
{
    public interface IGroupClient
    {
        Task<ApiResponse<List<GroupResponse>>> GetAllAsync();
        Task<ApiResponse<PagedResponse<GroupResponse>>> GetPageAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<GroupResponse>> GetAsync(int id);
        Task<ApiResponse<GroupResponse>> CreateAsync(GroupRequest Group);
        Task<ApiResponse<GroupResponse>> UpdateAsync(GroupRequest Group);
        Task DeleteAsync(int id);
        Task<ApiResponse<List<NatureResponse>>> GetNatures();
        Task<ApiResponse<List<GroupResponse>>> GetGroupsByNature(int typeId);
    }
}
