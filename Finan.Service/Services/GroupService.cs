using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class GroupService : BaseContractService<Group>, IGroupService
    {
        private readonly IGroupRepository _baseRepository;

        public GroupService(IGroupRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<PagedResult<GroupDTO>> GetGroupsAsync(int pageNumber, int pageSize) => await _baseRepository.GetGroupsAsync(pageNumber, pageSize);

    }
}
