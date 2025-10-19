using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        Task<GroupDTO> CreateGroup(GroupCommand groupCommand);
        Task<PagedResult<GroupDTO>> GetGroupsAsync(int pageNumber, int pageSize);
        Task<GroupDTO> UpdateGroup(GroupCommand groupCommand);
    }
}
