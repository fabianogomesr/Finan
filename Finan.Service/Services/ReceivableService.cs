using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class ReceivableService : BaseService<Receivable>, IReceivableService
    {
        private readonly IReceivableRepository _baseRepository;
        private readonly IBaseRepository<FinancialGroup> _financialGroupRepository;
        private readonly IFinancialClassificationRepository _financialClassificationRepository;
        private readonly IBaseRepository<Currency> _currencyRepository;
        private readonly IBaseRepository<CostCenter> _costCenterRepository;
        private readonly IUserRepository _userRepository;

        public ReceivableService(IReceivableRepository baseRepository,
            IBaseRepository<FinancialGroup> financialGroupRepository,
            IFinancialClassificationRepository financialClassificationRepository,
            IUserRepository userRepository,
            IBaseRepository<Currency> currencyRepository,
            IBaseRepository<CostCenter> costCenterRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _financialGroupRepository = financialGroupRepository;
            _financialClassificationRepository = financialClassificationRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _costCenterRepository = costCenterRepository;
        }

        public async Task<ReceivableDTO?> AddReceivable(ReceivableCommand receivableParameter)
        {            
            var financialGroup = await _financialGroupRepository.Select(receivableParameter.FinancialGroupId);
            var financialClassification = await _financialClassificationRepository.Select(receivableParameter.FinancialClassificationId);
            var costCenter = await _costCenterRepository.Select(receivableParameter.CostCenterId);
            var currency = await _currencyRepository.Select(receivableParameter.CurrencyId);

            Receivable receivable = new Receivable
            {
                CostCenter = costCenter,
                FinancialGroup = financialGroup,
                FinancialClassification = financialClassification,
                Currency = currency,
                Description = receivableParameter.Description, 
                Type = (Domain.Enums.TransactionType)receivableParameter.Type,
                Value = receivableParameter.Value,
                Discount = receivableParameter.Discount,
                TotalReceivable = receivableParameter.TotalReceivable,
                IssueDate = receivableParameter.IssueDate,
                DueDate = receivableParameter.DueDate,
                CashFlowDate = receivableParameter.CashFlowDate,
                AccrualPeriodDate = receivableParameter.AccrualPeriodDate,
                Observation = receivableParameter.Observation,
                Status = (Domain.Enums.TransactionStatus)receivableParameter.Status
            };
           
            await _baseRepository.Insert(receivable);

            return new ReceivableDTO
            {
                Id = receivable.Id,
                CostCenterId = receivable.CostCenter.Id,
                FinancialGroupId = receivable.FinancialGroup.Id,
                FinancialClassificationId = receivable.FinancialClassification.Id,
                CurrencyId = receivable.Currency.Id,
                Type = (byte)receivable.Type.GetHashCode(),
                Value = receivable.Value,
                Discount = receivable.Discount,
                TotalReceivable = receivable.TotalReceivable,
                IssueDate = receivable.IssueDate,
                DueDate = receivable.DueDate,
                CashFlowDate = receivable.CashFlowDate,
                AccrualPeriodDate = receivable.AccrualPeriodDate,
                Observation = receivable.Observation,
                Status = (byte)receivable.Status.GetHashCode()
            };
        }

        public async Task<List<ReceivableDTO>> CreateReceivablesAsync(List<ReceivableCommand> receivables)
        {
            List<ReceivableDTO> result = new List<ReceivableDTO>();

            foreach (var receivable in receivables)
            {
                var addResult = await AddReceivable(receivable);

                if(addResult != null)
                    result.Add(addResult);
            }

            return result;
        }

        public async Task<ReceivableDTO> GetReceivableByIdAsync(int id)
        {
            var result = await _baseRepository.GetReceivableByIdAsync(id);

            if (result == null)
                return null;

            return new ReceivableDTO
            {
                Id = result.Id,
                CostCenterId = result.Id,
                FinancialGroupId = result.FinancialGroup.Id,
                FinancialClassificationId = result.FinancialClassification.Id,
                CurrencyId = result.Currency.Id,
                Type = (byte)result.Type.GetHashCode(),
                Value = result.Value,
                Discount = result.Discount,
                TotalReceivable = result.TotalReceivable,
                IssueDate = result.IssueDate,
                DueDate = result.DueDate,
                CashFlowDate = result.CashFlowDate,
                AccrualPeriodDate = result.AccrualPeriodDate,
                Observation = result.Observation,
                Status = (byte)result.Status.GetHashCode()
            };
        }

        public async Task<ReceivableDTO?> UpdateReceivable(ReceivableCommand receivableParameter)
        {
            var receivable = _baseRepository.Select(receivableParameter.Id).Result;

            if (receivable == null)
                return null;

            receivable.CostCenter = _costCenterRepository.Select(receivableParameter.CostCenterId).Result;
            receivable.FinancialGroup = _financialGroupRepository.Select(receivableParameter.FinancialGroupId).Result;
            receivable.FinancialClassification = _financialClassificationRepository.Select(receivableParameter.FinancialClassificationId).Result;
            receivable.Currency = _currencyRepository.Select(receivableParameter.CurrencyId).Result;
            receivable.Type = (Domain.Enums.TransactionType)receivableParameter.Type;
            receivable.Value = receivableParameter.Value;
            receivable.Discount = receivableParameter.Discount;
            receivable.TotalReceivable = receivableParameter.TotalReceivable;
            receivable.IssueDate = receivableParameter.IssueDate;
            receivable.DueDate = receivableParameter.DueDate;
            receivable.CashFlowDate = receivableParameter.CashFlowDate;
            receivable.AccrualPeriodDate = receivableParameter.AccrualPeriodDate;
            receivable.Observation = receivableParameter.Observation;
            receivable.Status = (Domain.Enums.TransactionStatus)receivableParameter.Status; 

            await _baseRepository.Update(receivable);

            return new ReceivableDTO
            {
                Id = receivable.Id,
                CostCenterId = receivable.CostCenter.Id,
                FinancialGroupId = receivable.FinancialGroup.Id,
                FinancialClassificationId = receivable.FinancialClassification.Id,
                CurrencyId = receivable.Currency.Id,
                Type = (byte)receivable.Type.GetHashCode(),
                Value = receivable.Value,
                Discount = receivable.Discount,
                TotalReceivable = receivable.TotalReceivable,
                IssueDate = receivable.IssueDate,
                DueDate = receivable.DueDate,
                CashFlowDate = receivable.CashFlowDate,
                AccrualPeriodDate = receivable.AccrualPeriodDate,
                Observation = receivable.Observation,
                Status = (byte)receivable.Status.GetHashCode()
            };
        }
    }
}
