using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Filters;
using Finan.Domain.Interfaces;
using Finan.Service.Validators;
using FluentValidation;

namespace Finan.Service.Services
{
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _baseRepository;

        public TransactionService(ITransactionRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<TransactionDTO> AddTransaction(TransactionCommand TransactionParameter)
        {
            Transaction Transaction = new Transaction
            {
                CostCenterId = TransactionParameter.CostCenterId,
                GroupId = TransactionParameter.GroupId,
                ClassificationId = TransactionParameter.ClassificationId,
                CurrencyId = TransactionParameter.CurrencyId,
                Description = TransactionParameter.Description,
                Type = (TransactionType)TransactionParameter.TypeId,
                Value = TransactionParameter.Value,
                Discount = TransactionParameter.Discount,
                LateFee = TransactionParameter.LateTransactions,
                TotalPaid = TransactionParameter.TotalPaid,
                IssueDate = TransactionParameter.IssueDate,
                DueDate = TransactionParameter.DueDate,
                CashFlowDate = TransactionParameter.CashFlowDate,
                AccrualPeriodDate = TransactionParameter.AccrualPeriodDate,
                Observation = TransactionParameter.Observation,
                Status = (TransactionStatus)TransactionParameter.StatusId
            };

            await _baseRepository.Insert(Transaction);

            return new TransactionDTO
            {
                Id = Transaction.Id,
                CostCenterId = Transaction.CostCenterId,
                GroupId = Transaction.GroupId,
                ClassificationId = Transaction.ClassificationId,
                CurrencyId = Transaction.CurrencyId,
                TypeId = (byte)Transaction.Type.GetHashCode(),
                Value = Transaction.Value,
                Discount = Transaction.Discount,
                LateFee = Transaction.LateFee,
                TotalPaid = Transaction.TotalPaid,
                IssueDate = Transaction.IssueDate,
                DueDate = Transaction.DueDate,
                CashFlowDate = Transaction.CashFlowDate,
                AccrualPeriodDate = Transaction.AccrualPeriodDate,
                Observation = Transaction.Observation,
                StatusId = (byte)Transaction.Status.GetHashCode()
            };
        }

        public async Task<List<TransactionDTO>> GetTransactionsAsync()
        {
            var result = await _baseRepository.GetTransactionsAsync();

            if (result == null)
                return null;

            return result.Select(x => new TransactionDTO
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                GroupId = x.GroupId,
                ClassificationId = x.ClassificationId,
                CurrencyId = x.CurrencyId,
                TypeId = (byte)x.Type.GetHashCode(),
                Value = x.Value,
                Discount = x.Discount,
                LateFee = x.LateFee,
                TotalPaid = x.TotalPaid,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate,
                CashFlowDate = x.CashFlowDate,
                AccrualPeriodDate = x.AccrualPeriodDate,
                Observation = x.Observation,
                StatusId = (byte)x.Status.GetHashCode()
            }).ToList();
        }

        public async Task<TransactionDTO> GetTransactionByIdAsync(int id)
        {
            var result = await _baseRepository.GetTransactionByIdAsync(id);

            if (result == null)
                return null;

            return new TransactionDTO
            {
                Id = result.Id,
                CostCenterId = result.CostCenterId,
                GroupId = result.GroupId,
                ClassificationId = result.ClassificationId,
                CurrencyId = result.CurrencyId,
                TypeId = (byte)result.Type.GetHashCode(),
                Description = result.Description,
                Value = result.Value,
                Discount = result.Discount,
                LateFee = result.LateFee,
                TotalPaid = result.TotalPaid,
                IssueDate = result.IssueDate,
                DueDate = result.DueDate,
                CashFlowDate = result.CashFlowDate,
                AccrualPeriodDate = result.AccrualPeriodDate,
                Observation = result.Observation,
                StatusId = (byte)result.Status.GetHashCode()
            };
        }

        public async Task<TransactionDTO> UpdateTransaction(TransactionCommand TransactionParameter)
        {
            var Transaction = _baseRepository.Select(TransactionParameter.Id).Result;

            if (Transaction == null)
                throw new Exception("Pagamento não localizado.");

            Transaction.CostCenterId = TransactionParameter.CostCenterId;
            Transaction.GroupId = TransactionParameter.GroupId;
            Transaction.ClassificationId = TransactionParameter.ClassificationId;
            Transaction.CurrencyId = TransactionParameter.CurrencyId;
            Transaction.Type = (TransactionType)TransactionParameter.TypeId;
            Transaction.Description = TransactionParameter.Description;
            Transaction.Value = TransactionParameter.Value;
            Transaction.Discount = TransactionParameter.Discount;
            Transaction.LateFee = TransactionParameter.LateTransactions;
            Transaction.TotalPaid = TransactionParameter.TotalPaid;
            Transaction.IssueDate = TransactionParameter.IssueDate;
            Transaction.DueDate = TransactionParameter.DueDate;
            Transaction.CashFlowDate = TransactionParameter.CashFlowDate;
            Transaction.AccrualPeriodDate = TransactionParameter.AccrualPeriodDate;
            Transaction.Observation = TransactionParameter.Observation;
            Transaction.Status = (TransactionStatus)TransactionParameter.StatusId;

            await _baseRepository.Update(Transaction);

            return new TransactionDTO
            {
                Id = Transaction.Id,
                CostCenterId = Transaction.CostCenterId,
                GroupId = Transaction.GroupId,
                ClassificationId = Transaction.ClassificationId,
                CurrencyId = Transaction.CurrencyId,
                TypeId = (byte)Transaction.Type.GetHashCode(),
                Value = Transaction.Value,
                Discount = Transaction.Discount,
                LateFee = Transaction.LateFee,
                TotalPaid = Transaction.TotalPaid,
                IssueDate = Transaction.IssueDate,
                DueDate = Transaction.DueDate,
                CashFlowDate = Transaction.CashFlowDate,
                AccrualPeriodDate = Transaction.AccrualPeriodDate,
                Observation = Transaction.Observation,
                StatusId = (byte)Transaction.Status.GetHashCode()
            };
        }

        public async Task<PagedResult<TransactionDTO>> GetTransactionsAsync(TransactionFilter filter) => await _baseRepository.GetTransactionsAsync(filter);

        public List<TransactionTypeDTO> GetTypeList()
        {
            var result = EnumExtensions.GetEnumList<TransactionType>();

            return result.Select(x => new TransactionTypeDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }

        public List<TransactionStatusDTO> GetStatusList()
        {
            var result = EnumExtensions.GetEnumList<TransactionStatus>();

            return result.Select(x => new TransactionStatusDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }

        public List<DateTypeDTO> GetDateTypeList()
        {
            var result = EnumExtensions.GetEnumList<DateTypeEnum>();

            return result.Select(x => new DateTypeDTO
            {
                TypeId = x.Value,
                Name = x.Description
            }).ToList();

        }

    }
}

