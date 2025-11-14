using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Validators;

namespace Finan.Service.Services
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly IGroupRepository _baseRepository;

        public GroupService(IGroupRepository baseRepository) 
        {
            _baseRepository = baseRepository;
        }

        public async Task<GroupDTO?> CreateGroup(GroupCommand groupCommand)
        {
            if (!Validate(groupCommand, new GroupValidator()))
                return null;

            var group = new Group
            {
                Description = groupCommand.Description,
                Nature = groupCommand.NatureId
            };

            await _baseRepository.Insert(group);

            return new GroupDTO
            {
                Id = group.Id,
                Description = group.Description,
                Nature = group.Nature.GetDescription(),
                NatureId = (byte)group.Nature
            };
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
            {
                Messages.Error("Grupo não encontrada.");
                return;
            }

            await _baseRepository.Delete(id);
        }

        public async Task<List<GroupDTO>?> GetAsync()
        {
            var result = await _baseRepository.Select();

            if (!result.Any())
                return null;

            return result.Select(x => new GroupDTO
            {
                Id = x.Id,
                Description = x.Description,
                Nature = x.Nature.GetDescription(),
                NatureId = (byte)x.Nature
            }).ToList();
        }

        public async Task<GroupDTO?> GetAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
                return null;

            return new GroupDTO
            {
                Id = result.Id,
                Description = result.Description,
                Nature = result.Nature.GetDescription(),
                NatureId = (byte)result.Nature
            };
        }

        public async Task<PagedResult<GroupDTO>?> GetGroupsAsync(int pageNumber, int pageSize) 
        {
            var result = await _baseRepository.GetGroupsAsync(pageNumber, pageSize);

            if (!result.Items.Any())
                return null;

            return result;
        }

        public async Task<List<GroupDTO>?> GetGroupsByNatureId(NatureGroup natureId)
        {
            var result = _baseRepository.GetAll().Where(x => x.Nature == natureId).ToList();

            if (!result.Any())
                return null;

            return result.Select(x => new GroupDTO
            {
                Id = x.Id,
                Description = x.Description,
                Nature = x.Nature.GetDescription(),
                NatureId = (byte)x.Nature
            }).ToList();
        }

        public List<NatureDTO>? GetNatureList()
        {
            var result = EnumExtensions.GetEnumList<NatureGroup>();

            if (!result.Any())
                return null;

            return result.Select(x => new NatureDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }

        public async Task<GroupDTO?> UpdateGroup(GroupCommand groupCommand)
        {
            if (!Validate(groupCommand, new GroupValidator()))
                return null;

            var group = await _baseRepository.Select(groupCommand.Id);

            if (group == null)
            {
                Messages.Error("Grupo não encontrado.");
                return null;
            }

            group.Description = groupCommand.Description;
            group.Nature = groupCommand.NatureId;

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
