using Finan.Domain.Commands;
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
    public interface ITransactionService : IBaseService<Transaction>
    {
        Task<TransactionDTO> AddTransaction(TransactionCommand TransactionParameter);
        Task<TransactionDTO> GetTransactionByIdAsync(int id);
        Task<List<TransactionDTO>> GetTransactionsAsync();
        Task<PagedResult<TransactionDTO>> GetTransactionsAsync(TransactionFilter filter);
        List<TransactionTypeDTO> GetTypeList();
        List<TransactionStatusDTO> GetStatusList();
        Task<TransactionDTO> UpdateTransaction(TransactionCommand TransactionParameter);
        List<DateTypeDTO> GetDateTypeList();
    }
}
