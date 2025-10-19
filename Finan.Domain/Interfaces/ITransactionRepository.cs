using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetTransactionsAsync();
        Task<PagedResult<TransactionDTO>> GetTransactionsAsync(TransactionFilter filter);
    }
}
