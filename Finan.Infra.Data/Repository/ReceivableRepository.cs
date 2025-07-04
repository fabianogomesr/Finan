using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
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
                .Include(x => x.Currency)
                .Where(x => x.Id == id);

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
            var entities = await _dbSet.Set<Receivable>()
            .Include(x => x.CostCenter)
            .Include(x => x.FinancialGroup)
            .Include(x => x.FinancialClassification)
            .Include(x => x.Currency)
            .AsQueryable()
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

        public async Task<List<ReceivableSummaryClassificationDTO>> GetReceivableSummaryClassificationByMonthYear(int month, int year)
        {
            var result = await _dbSet.Receivable
                .Where(x => x.DueDate.Month == month && x.DueDate.Year == year && (int)x.Status == (int)ReceivableStatus.Received)
                .GroupBy(x => new { x.FinancialClassificationId, x.FinancialClassification.Description })
                .Select(g => new ReceivableSummaryClassificationDTO
                {
                    Classification = g.Key.Description,
                    TotalAmount = g.Sum(x => x.Value - x.Discount),
                    TotalCount = g.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<ReceivableSummaryDTO> GetReceivableSummaryByMonthYear(int month, int year)
        {
            var result = await _dbSet.Receivable
                .Where(x => x.DueDate.Month == month && x.DueDate.Year == year && (int)x.Status == (int)ReceivableStatus.Received)
                .GroupBy(x => new { x.DueDate.Month, x.DueDate.Year })
                .Select(g => new ReceivableSummaryDTO
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalAmount = g.Sum(x => x.Value - x.Discount),
                    TotalCount = g.Count()
                })
                .FirstOrDefaultAsync();

            return result ?? new ReceivableSummaryDTO { Month = month, Year = year, TotalAmount = 0, TotalCount = 0 };
        }
    }
}
