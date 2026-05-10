using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;

namespace Finan.Infra.Data.Repository
{
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        protected readonly BaseContext _dbSet;

        public CurrencyRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<PagedResult<CurrencyResponse>> GetBanksAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await GetAll().Select(x => new CurrencyResponse
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Symbol = x.Symbol
            }).ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
