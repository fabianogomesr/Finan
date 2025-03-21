using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Repository
{
    public class FinancialClassificationRepository : BaseRepository<FinancialClassification>, IFinancialClassificationRepository
    {
        protected readonly BaseContext _dbSet;

        public FinancialClassificationRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<FinancialClassification> GetFinancialClassificationByIdAsync(int id) => await _dbSet.Set<FinancialClassification>().Include(x => x.FinancialGroup).FirstOrDefaultAsync(x => x.Id == id);
        public async Task<IEnumerable<FinancialClassification>> GetFinancialClassificationsAsync() => await _dbSet.Set<FinancialClassification>().Include(x => x.FinancialGroup).ToListAsync();
    }
}
