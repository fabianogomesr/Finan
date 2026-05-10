using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finan.Contracts.Filters;

namespace Finan.Domain.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetTransactionsAsync();
        Task<PagedResult<TransactionResponse>> GetTransactionsAsync(TransactionFilter filter);
    }
}
