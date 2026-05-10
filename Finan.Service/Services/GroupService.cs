using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Application.Validators;


namespace Finan.Application.Services
{
    public class GroupService : BaseService, IGroupService
    {
        private readonly IGroupRepository _baseRepository;

        public GroupService(IGroupRepository baseRepository) 
        {
            _baseRepository = baseRepository;
        }

        public async Task<GroupResponse?> CreateGroup(GroupRequest groupCommand)
        {
            if (!Validate(groupCommand, new GroupValidator()))
                return null;

            var group = new Group
            {
                Description = groupCommand.Description,
                Nature = groupCommand.NatureId
            };

            await _baseRepository.Insert(group);

            return new GroupResponse
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

        public async Task<List<GroupResponse>?> GetAsync()
        {
            var result = await _baseRepository.Select();

            if (!result.Any())
                return null;

            return result.Select(x => new GroupResponse
            {
                Id = x.Id,
                Description = x.Description,
                Nature = x.Nature.GetDescription(),
                NatureId = (byte)x.Nature
            }).ToList();
        }

        public async Task<GroupResponse?> GetAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
                return null;

            return new GroupResponse
            {
                Id = result.Id,
                Description = result.Description,
                Nature = result.Nature.GetDescription(),
                NatureId = (byte)result.Nature
            };
        }

        public async Task<PagedResult<GroupResponse>?> GetGroupsAsync(int pageNumber, int pageSize) 
        {
            var result = await _baseRepository.GetGroupsAsync(pageNumber, pageSize);

            return result;
        }

        public async Task<List<GroupResponse>?> GetGroupsByNatureId(NatureGroup natureId)
        {
            var result = _baseRepository.GetAll().Where(x => x.Nature == natureId).ToList();

            if (!result.Any())
                return null;

            return result.Select(x => new GroupResponse
            {
                Id = x.Id,
                Description = x.Description,
                Nature = x.Nature.GetDescription(),
                NatureId = (byte)x.Nature
            }).ToList();
        }

        public List<NatureResponse>? GetNatureList()
        {
            var result = EnumExtensions.GetEnumList<NatureGroup>();

            if (!result.Any())
                return null;

            return result.Select(x => new NatureResponse
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }

        public async Task<GroupResponse?> UpdateGroup(GroupRequest groupCommand)
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

            return new GroupResponse
            {
                Id = group.Id,
                Description = group.Description,
                Nature = group.Nature.GetDescription(),
                NatureId = (byte)group.Nature
            };
        }
    }
}
