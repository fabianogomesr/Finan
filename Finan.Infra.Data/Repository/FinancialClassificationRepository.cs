using Finan.Domain.Entities;
using Finan.Domain.Enums;
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

        public async Task<IEnumerable<FinancialClassification>> GetClassificationsByGroupIdAndTypeAsync(int financialGroupId, ClassificationType classificationType) {

            var result = _dbSet.Set<FinancialClassification>().Include(x => x.FinancialGroup).Where(x => x.FinancialGroupId == financialGroupId);

            if (classificationType == ClassificationType.Income)
                result = result.Where(x => (x.Type == ClassificationType.Income || x.Type == ClassificationType.Both));
            else if (classificationType == ClassificationType.Expense)
                result = result.Where(x => (x.Type == ClassificationType.Expense || x.Type == ClassificationType.Both));

            return await result.ToListAsync();
        } 

        public async Task<FinancialClassification> GetFinancialClassificationByIdAsync(int id) => await _dbSet.Set<FinancialClassification>().Include(x => x.FinancialGroup).FirstAsync(x => x.Id == id);
        public async Task<IEnumerable<FinancialClassification>> GetFinancialClassificationsAsync() => await _dbSet.Set<FinancialClassification>().Include(x => x.FinancialGroup).ToListAsync();

        public async Task<EntityPagination<FinancialClassification>> GetFinancialClassificationsAsync(int pageNumber = 1, int pageSize = 5)
        {
            var entities = await _dbSet.Set<FinancialClassification>().AsQueryable()
            .Include(x => x.FinancialGroup)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var totalItems = await _dbSet.Set<FinancialClassification>().CountAsync();

            return new EntityPagination<FinancialClassification>
            {
                Entities = entities,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
