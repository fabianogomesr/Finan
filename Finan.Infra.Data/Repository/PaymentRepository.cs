using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Filters;
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
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        protected new readonly BaseContext _dbSet;

        public PaymentRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            var result = _dbSet.Payment.Include(x => x.CostCenter)
                .Include(x => x.FinancialGroup)
                .Include(x => x.FinancialClassification)
                .Include(x => x.Currency)
                .Where(x => x.Id == id);

            return await result.FirstAsync();
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            var result = _dbSet.Payment.Include(x => x.CostCenter)
                .Include(x => x.FinancialGroup)
                .Include(x => x.FinancialClassification)
                .Include(x => x.Currency);

            return await result.ToListAsync();
        }

        public async Task<EntityPagination<Payment>> GetPaymentsAsync(PaymentFilter filter)
        {
            var query = _dbSet.Set<Payment>()
            .Include(x => x.CostCenter)
            .Include(x => x.FinancialGroup)
            .Include(x => x.FinancialClassification)
            .Include(x => x.Currency)
            .AsQueryable();

            if (filter.StartDate != null && filter.EndDate != null)
            {
                if(filter.DateType == Domain.Enums.DateTypeEnum.Issue)
                {
                    query = query.Where(x => x.IssueDate >= filter.StartDate && x.IssueDate <= filter.EndDate);
                }
                else if(filter.DateType == Domain.Enums.DateTypeEnum.Due)
                {
                    query = query.Where(x => x.DueDate >= filter.StartDate && x.DueDate <= filter.EndDate);
                }
                else if (filter.DateType == Domain.Enums.DateTypeEnum.CashFlow)
                {
                    query = query.Where(x => x.CashFlowDate >= filter.StartDate && x.CashFlowDate <= filter.EndDate);
                }
                else if (filter.DateType == Domain.Enums.DateTypeEnum.AccrualPeriod)
                {
                    query = query.Where(x => x.AccrualPeriodDate >= filter.StartDate && x.AccrualPeriodDate <= filter.EndDate);
                }
            }

            var totalItems = await query.CountAsync();

            var result = await query.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();


            return new EntityPagination<Payment>
            {
                Entities = result,
                TotalItems = totalItems,
                CurrentPage = filter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)filter.PageSize)
            };
        }
    }
}
