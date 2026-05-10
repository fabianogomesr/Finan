using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;
using Finan.Contracts.Filters;

namespace Finan.Domain.Interfaces
{
    public interface ITransactionService : IBaseService
    {
        Task<TransactionResponse> AddTransaction(TransactionRequest TransactionParameter);
        Task<TransactionResponse> GetTransactionByIdAsync(int id);
        Task<List<TransactionResponse>> GetTransactionsAsync();
        Task<PagedResult<TransactionResponse>> GetTransactionsAsync(TransactionFilter filter);
        List<TransactionTypeResponse> GetTypeList();
        List<TransactionStatusResponse> GetStatusList();
        Task<TransactionResponse> UpdateTransaction(TransactionRequest TransactionParameter);
        List<DateTypeResponse> GetDateTypeList();
    }
}
