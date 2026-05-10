using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<PagedResult<GroupResponse>> GetGroupsAsync(int pageNumber, int pageSize);
    }
}
