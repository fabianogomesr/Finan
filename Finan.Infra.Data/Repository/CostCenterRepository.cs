using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;

namespace Finan.Infra.Data.Repository
{
    public class CostCenterRepository : BaseRepository<CostCenter>, ICostCenterRepository
    {
        protected readonly BaseContext _dbSet;

        public CostCenterRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<PagedResult<CostCenterDTO>> GetBanksAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await GetAll().Select(x => new CostCenterDTO 
            {
                Id = x.Id,
                Description = x.Description
            }).ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
