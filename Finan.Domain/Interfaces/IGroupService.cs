using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        Task<PagedResult<GroupDTO>> GetGroupsAsync(int pageNumber, int pageSize);
    }
}
