using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class GroupService : BaseService<Group>, IGroupService
    {
        private readonly IGroupRepository _baseRepository;

        public GroupService(IGroupRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<GroupDTO> CreateGroup(GroupCommand groupCommand)
        {
            var group = new Group
            {
                Description = groupCommand.Description,
                Nature = (Domain.Enums.NatureGroup)groupCommand.NatureId
            };

            Validate(group, new GroupValidator());

            await _baseRepository.Insert(group);

            return new GroupDTO
            {
                Id = group.Id,
                Description = group.Description,
                Nature = group.Nature.GetDescription(),
                NatureId = (byte)group.Nature
            };
        }

        public async Task<PagedResult<GroupDTO>> GetGroupsAsync(int pageNumber, int pageSize) => await _baseRepository.GetGroupsAsync(pageNumber, pageSize);

        public async Task<GroupDTO> UpdateGroup(GroupCommand groupCommand)
        {
            var group = await _baseRepository.Select(groupCommand.Id);

            if (group == null)
                throw new KeyNotFoundException("Group not found.");

            group.Description = groupCommand.Description;
            group.Nature = (Domain.Enums.NatureGroup)groupCommand.NatureId;

            Validate(group, new GroupValidator());

            await _baseRepository.Update(group);

            return new GroupDTO
            {
                Id = group.Id,
                Description = group.Description,
                Nature = group.Nature.GetDescription(),
                NatureId = (byte)group.Nature
            };
        }
    }
}
