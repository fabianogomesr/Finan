using Finan.Domain.Entities;
using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;

namespace Finan.Domain.Interfaces
{
    public interface IGroupService : IBaseService
    {
        Task<GroupResponse?> CreateGroup(GroupRequest groupCommand);
        Task DeleteAsync(int id);
        Task<List<GroupResponse>?> GetAsync();
        Task<GroupResponse?> GetAsync(int id);
        Task<PagedResult<GroupResponse>?> GetGroupsAsync(int pageNumber, int pageSize);
        Task<List<GroupResponse>?> GetGroupsByNatureId(NatureGroup natureId);
        List<NatureResponse?> GetNatureList();
        Task<GroupResponse?> UpdateGroup(GroupRequest groupCommand);
    }
}
