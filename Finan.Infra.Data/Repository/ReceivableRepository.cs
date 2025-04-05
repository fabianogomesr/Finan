using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Repository
{
    public class ReceivableRepository : BaseRepository<Receivable>, IReceivableRepository
    {
        protected readonly BaseContext _dbSet;

        public ReceivableRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<Receivable> GetReceivableByIdAsync(int id)
        {
            var result = _dbSet.Receivable.Include(x => x.CostCenter)
                .Include(x => x.FinancialGroup)
                .Include(x => x.FinancialClassification)
                .Include(x => x.Currency);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<Receivable>> GetReceivablesAsync()
        {
            var result = _dbSet.Receivable.Include(x => x.CostCenter)
                .Include(x => x.FinancialGroup)
                .Include(x => x.FinancialClassification)
                .Include(x => x.Currency);

            return await result.ToListAsync();
        }

        public async Task<EntityPagination<Receivable>> GetReceivablesAsync(int pageNumber, int pageSize)
        {
            var entities = await _dbSet.Set<Receivable>().AsQueryable()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var totalItems = await _dbSet.Set<Receivable>().CountAsync();

            return new EntityPagination<Receivable>
            {
                Entities = entities,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
