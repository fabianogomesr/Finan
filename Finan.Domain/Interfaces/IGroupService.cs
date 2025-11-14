using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Parameters;

namespace Finan.Domain.Interfaces
{
    public interface IGroupService : IBaseService
    {
        Task<GroupDTO?> CreateGroup(GroupCommand groupCommand);
        Task DeleteAsync(int id);
        Task<List<GroupDTO>?> GetAsync();
        Task<GroupDTO?> GetAsync(int id);
        Task<PagedResult<GroupDTO>?> GetGroupsAsync(int pageNumber, int pageSize);
        Task<List<GroupDTO>?> GetGroupsByNatureId(NatureGroup natureId);
        List<NatureDTO?> GetNatureList();
        Task<GroupDTO?> UpdateGroup(GroupCommand groupCommand);
    }
}
