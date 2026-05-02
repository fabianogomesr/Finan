using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Filters;
using Finan.Domain.Interfaces;
using Finan.Service.Validators;
using System.Data.Common;
using System.Reflection.Metadata;

namespace Finan.Service.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly ITransactionRepository _baseRepository;
        private readonly IStatementRepository _statementRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository baseRepository, IStatementRepository statementRepository, IAccountRepository accountRepository)
        {
            _baseRepository = baseRepository;
            _statementRepository = statementRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<TransactionDTO>?> GetTransactionsAsync()
        {
            var result = await _baseRepository.GetTransactionsAsync();

            if (!result.Any())
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

        public async Task<TransactionDTO?> GetTransactionByIdAsync(int id)
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
                StatusId = (byte)result.Status.GetHashCode(),
                PaidTransaction = new PaidTransaction
                {
                    PaidDate = result.Statements.FirstOrDefault().FlowDate,
                    PaidValue = result.Statements.FirstOrDefault().Value,
                    AccountId = result.Statements.FirstOrDefault().AccountId.GetValueOrDefault(),
                },
            };
        }

        public async Task<TransactionDTO?> AddTransaction(TransactionCommand transactionCommand)
        {
            if (!Validate(transactionCommand, new TransactionValidator()))
                return null;

            var transaction = CreateTransactionFields(transactionCommand);

            await _baseRepository.Insert(transaction);

            try
            {
                // Gerencia os efeitos colaterais de acordo com o status
                await HandleTransactionStatusEffects(transaction, transactionCommand);
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
                return null;
            }

            return MapToTransactionDTO(transaction);
        }

        private static Transaction CreateTransactionFields(TransactionCommand TransactionParameter)
        {
            return new Transaction
            {
                CostCenterId = TransactionParameter.CostCenterId,
                GroupId = TransactionParameter.GroupId,
                ClassificationId = TransactionParameter.ClassificationId,
                CurrencyId = TransactionParameter.CurrencyId,
                Description = TransactionParameter.Description,
                Type = (TransactionType)TransactionParameter.TypeId,
                Value = TransactionParameter.Value,
                Discount = TransactionParameter.Discount,
                LateFee = TransactionParameter.LateFee,
                TotalPaid = TransactionParameter.TotalPaid,
                IssueDate = TransactionParameter.IssueDate,
                DueDate = TransactionParameter.DueDate,
                CashFlowDate = TransactionParameter.CashFlowDate,
                AccrualPeriodDate = TransactionParameter.AccrualPeriodDate,
                Observation = TransactionParameter.Observation,
                Status = (TransactionStatus)TransactionParameter.StatusId
            };
        }

        public async Task<TransactionDTO?> UpdateTransaction(TransactionCommand transactionCommand)
        {
            if (!Validate(transactionCommand, new TransactionValidator()))
                return null;

            // Recupera a transação existente
            var transaction = await _baseRepository.Select(transactionCommand.Id);

            if (transaction == null)
            {
                Messages.Error("Transação não encontrada.");
                return null;
            }

            bool alreadyPaid = false;
            if(transactionCommand.StatusId == TransactionStatus.Paid && transactionCommand.StatusId == transaction.Status)
            {
                alreadyPaid = true;
            }

            // Atualiza os campos da transação
            UpdateTransactionFields(transaction, transactionCommand);

            try
            {
                // Gerencia os efeitos colaterais de acordo com o status
                await HandleTransactionStatusEffects(transaction, transactionCommand, alreadyPaid);
            }
            catch (Exception ex)
            {
                Messages.Error(ex.Message);
                return null;
            }

            await _baseRepository.Update(transaction);

            return MapToTransactionDTO(transaction);
        }

        private void UpdateTransactionFields(Transaction transaction, TransactionCommand parameter)
        {
            transaction.CostCenterId = parameter.CostCenterId;
            transaction.GroupId = parameter.GroupId;
            transaction.ClassificationId = parameter.ClassificationId;
            transaction.CurrencyId = parameter.CurrencyId;
            transaction.Type = (TransactionType)parameter.TypeId;
            transaction.Description = parameter.Description;
            transaction.Value = parameter.Value;
            transaction.Discount = parameter.Discount;
            transaction.LateFee = parameter.LateFee;
            transaction.TotalPaid = parameter.TotalPaid;
            transaction.IssueDate = parameter.IssueDate;
            transaction.DueDate = parameter.DueDate;
            transaction.CashFlowDate = parameter.CashFlowDate;
            transaction.AccrualPeriodDate = parameter.AccrualPeriodDate;
            transaction.Observation = parameter.Observation;
            transaction.Status = (TransactionStatus)parameter.StatusId;
        }

        private async Task HandleTransactionStatusEffects(Transaction transaction, TransactionCommand parameter, bool alreadyPaid)
        {
            if (alreadyPaid)
            {
                await HandleCanceledOrOpenTransaction(transaction);
            }

            if (transaction.Status == TransactionStatus.Paid)
            {
                await HandlePaidTransaction(transaction, parameter);
            }
            else if (transaction.Status == TransactionStatus.Canceled || transaction.Status == TransactionStatus.Open)
            {
                await HandleCanceledOrOpenTransaction(transaction);
            }
        }

        private async Task HandlePaidTransaction(Transaction transaction, TransactionCommand parameter)
        {
            var account = await _accountRepository.Select(parameter.PaidTransaction.AccountId);
            if (account == null)
                throw new Exception("Conta não localizada.");

            transaction.TotalPaid = parameter.PaidTransaction.PaidValue;

            var balance = _statementRepository.GetBalanceByAccountId(parameter.PaidTransaction.AccountId);
            var statement = new Statement(parameter.PaidTransaction.PaidDate, parameter.PaidTransaction.PaidValue, balance, parameter.PaidTransaction.AccountId, transaction);
            await _statementRepository.Insert(statement);

            account.Balance = statement.Balance;
            await _accountRepository.Update(account);
        }

        private async Task HandleCanceledOrOpenTransaction(Transaction transaction)
        {
            var statement = _statementRepository.GetAll()
                .FirstOrDefault(x => x.TransactionId == transaction.Id && !x.Reversed);

            if (statement != null)
            {
                if (statement.ReconciledDate != null)
                    throw new Exception("Não é possível cancelar uma transação conciliada. Favor desfazer a conciliação antes de cancelar.");

                statement.Reversed = true;
                await _statementRepository.Update(statement);
            }

            transaction.TotalPaid = 0;

            var account = await _accountRepository.GetAccountByIdAsync(statement.AccountId.GetValueOrDefault());
            if (account == null)
                throw new Exception("Conta não localizada.");

            var balance = _statementRepository.GetBalanceByAccountId(account.Id);
            account.Balance = balance;
            await _accountRepository.Update(account);
        }

        private TransactionDTO MapToTransactionDTO(Transaction transaction)
        {
            return new TransactionDTO
            {
                Id = transaction.Id,
                CostCenterId = transaction.CostCenterId,
                GroupId = transaction.GroupId,
                ClassificationId = transaction.ClassificationId,
                CurrencyId = transaction.CurrencyId,
                TypeId = (byte)transaction.Type.GetHashCode(),
                Value = transaction.Value,
                Discount = transaction.Discount,
                LateFee = transaction.LateFee,
                TotalPaid = transaction.TotalPaid,
                IssueDate = transaction.IssueDate,
                DueDate = transaction.DueDate,
                CashFlowDate = transaction.CashFlowDate,
                AccrualPeriodDate = transaction.AccrualPeriodDate,
                Observation = transaction.Observation,
                StatusId = (byte)transaction.Status.GetHashCode()
            };
        }

        public async Task<PagedResult<TransactionDTO>?> GetTransactionsAsync(TransactionFilter filter) 
        {
            var result = await _baseRepository.GetTransactionsAsync(filter);

            return result;
        } 

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
            var result = EnumExtensions.GetEnumList<DateType>();

            return result.Select(x => new DateTypeDTO
            {
                TypeId = x.Value,
                Name = x.Description
            }).ToList();
        }

    }
}

