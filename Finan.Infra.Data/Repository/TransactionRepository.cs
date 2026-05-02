using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Filters;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Finan.Infra.Data.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        protected new readonly BaseContext _dbSet;

        public TransactionRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            var result = GetAll().Include(x => x.CostCenter)
                .Include(x => x.Group)
                .Include(x => x.Classification)
                .Include(x => x.Currency)
                .Include(x => x.Statements)
                .Where(x => x.Id == id);

            return await result.FirstAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await GetAll()
                .Include(x => x.CostCenter)
                .Include(x => x.Group)
                .Include(x => x.Classification)
                .Include(x => x.Currency)
                .ToListAsync();
        }

        public async Task<PagedResult<TransactionDTO>> GetTransactionsAsync(TransactionFilter filter)
        {
            var query = GetAll();

            query.Where(x => x.Type == filter.TransactionType);

            if (filter.StartDate != null && filter.EndDate != null)
            {
                if (filter.DateType == DateType.Issue)
                {
                    query = query.Where(x => x.IssueDate >= filter.StartDate && x.IssueDate <= filter.EndDate);
                }
                else if (filter.DateType == DateType.Due)
                {
                    query = query.Where(x => x.DueDate >= filter.StartDate && x.DueDate <= filter.EndDate);
                }
                else if (filter.DateType == DateType.CashFlow)
                {
                    query = query.Where(x => x.CashFlowDate >= filter.StartDate && x.CashFlowDate <= filter.EndDate);
                }
                else if (filter.DateType == DateType.AccrualPeriod)
                {
                    query = query.Where(x => x.AccrualPeriodDate >= filter.StartDate && x.AccrualPeriodDate <= filter.EndDate);
                }
            }

            if (filter.Canceled)
            {
                query = query.Where(x => x.Status == TransactionStatus.Canceled);
            }
            else
            {
                query = query.Where(x => x.Status != TransactionStatus.Canceled);
            }

            return await query.Select(x => new TransactionDTO
            {
                Id = x.Id,
                Description = x.Description,
                CostCenterId = x.CostCenter != null ? x.CostCenter.Id : 0,
                CostCenterName = x.CostCenter != null ? x.CostCenter.Description : string.Empty,
                GroupId = x.Group != null ? x.Group.Id : 0,
                GroupName = x.Group != null ? x.Group.Description : String.Empty,
                ClassificationId = x.Classification != null ? x.Classification.Id : 0,
                ClassificationName = x.Classification != null ? x.Classification.Description : String.Empty,
                CurrencyId = x.CurrencyId,
                CurrencyName = x.Currency != null ? x.Currency.Code : String.Empty,
                TypeId = (byte)x.Type,
                TypeName = x.Type.GetDescription(),
                Value = x.Value,
                Discount = x.Discount,
                LateFee = x.LateFee,
                TotalPaid = x.TotalPaid,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate,
                CashFlowDate = x.CashFlowDate,
                AccrualPeriodDate = x.AccrualPeriodDate,
                Observation = x.Observation,
                StatusId = (byte)x.Status,
                StatusName = x.Status.GetDescription()
            }).ToPagedListAsync(filter.PageNumber, filter.PageSize);
        }
    }
}
