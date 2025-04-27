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
                .Include(x => x.Currency);

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

        public async Task<EntityPagination<Payment>> GetPaymentsAsync(int pageNumber, int pageSize)
        {
            var entities = await _dbSet.Set<Payment>()
            .Include(x => x.CostCenter)
            .Include(x => x.FinancialGroup)
            .Include(x => x.FinancialClassification)
            .Include(x => x.Currency)
            .AsQueryable()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var totalItems = await _dbSet.Set<Payment>().CountAsync();

            return new EntityPagination<Payment>
            {
                Entities = entities,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
