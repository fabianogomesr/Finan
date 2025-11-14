using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;

namespace Finan.Infra.Data.Repository
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        protected readonly BaseContext _dbSet;

        public BankRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<PagedResult<BankDTO>> GetBanksAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await GetAll().Select(x => new BankDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            }).ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
