using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;

namespace Finan.Infra.Data.Repository
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        protected readonly BaseContext _dbSet;

        public GroupRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<PagedResult<GroupDTO>> GetGroupsAsync(int pageNumber, int pageSize)
        {
            var query = GetAll();

            return await query.Select(x => new GroupDTO
            {
                Id = x.Id,
                Description = x.Description,
                Nature = x.Nature.GetDescription(),
                NatureId = (byte)x.Nature
            }).ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
