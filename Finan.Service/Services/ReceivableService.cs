using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;

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

        public async Task<ReceivableDTO?> AddReceivable<ReceivableValidator>(ReceivableCommand receivableParameter)
        {            
            Receivable receivable = new Receivable
            {
                CostCenterId = receivableParameter.CostCenterId,
                FinancialGroupId = receivableParameter.FinancialGroupId,
                FinancialClassificationId = receivableParameter.FinancialClassificationId,
                CurrencyId = receivableParameter.CurrencyId,
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
                Status = (Domain.Enums.ReceivableStatus)receivableParameter.Status
            };
           
            await _baseRepository.Insert(receivable);

            return new ReceivableDTO
            {
                Id = receivable.Id,
                CostCenterId = receivable.CostCenterId,
                FinancialGroupId = receivable.FinancialGroupId,
                FinancialClassificationId = receivable.FinancialClassificationId,
                CurrencyId = receivable.CurrencyId,
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

        public async Task<List<ReceivableDTO>> GetReceivablesAsync()
        {
            var result = await _baseRepository.GetReceivablesAsync();

            if (result == null)
                return null;

            return result.Select( x => new ReceivableDTO
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                FinancialGroupId = x.FinancialGroupId,
                FinancialClassificationId = x.FinancialClassificationId,
                CurrencyId = x.CurrencyId,
                Type = (byte)x.Type.GetHashCode(),
                Value = x.Value,
                Discount = x.Discount,
                TotalReceivable = x.TotalReceivable,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate,
                CashFlowDate = x.CashFlowDate,
                AccrualPeriodDate = x.AccrualPeriodDate,
                Observation = x.Observation,
                Status = (byte)x.Status.GetHashCode()
            }).ToList();
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
                FinancialGroupId = result.FinancialGroupId,
                FinancialClassificationId = result.FinancialClassificationId,
                CurrencyId = result.CurrencyId,
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

        public async Task<ReceivableDTO?> UpdateReceivable<ReceivableValidator>(ReceivableCommand receivableParameter)
        {
            var receivable = _baseRepository.Select(receivableParameter.Id).Result;

            if (receivable == null)
                return null;

            receivable.CostCenterId = receivableParameter.CostCenterId;
            receivable.FinancialGroupId = receivableParameter.FinancialGroupId;
            receivable.FinancialClassificationId = receivableParameter.FinancialClassificationId;
            receivable.CurrencyId = receivableParameter.CurrencyId;
            receivable.Type = (Domain.Enums.TransactionType)receivableParameter.Type;
            receivable.Value = receivableParameter.Value;
            receivable.Discount = receivableParameter.Discount;
            receivable.TotalReceivable = receivableParameter.TotalReceivable;
            receivable.IssueDate = receivableParameter.IssueDate;
            receivable.DueDate = receivableParameter.DueDate;
            receivable.CashFlowDate = receivableParameter.CashFlowDate;
            receivable.AccrualPeriodDate = receivableParameter.AccrualPeriodDate;
            receivable.Observation = receivableParameter.Observation;
            receivable.Status = (Domain.Enums.ReceivableStatus)receivableParameter.Status; 

            await _baseRepository.Update(receivable);

            return new ReceivableDTO
            {
                Id = receivable.Id,
                CostCenterId = receivable.CostCenterId,
                FinancialGroupId = receivable.FinancialGroupId,
                FinancialClassificationId = receivable.FinancialClassificationId,
                CurrencyId = receivable.CurrencyId,
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

        public async Task<ReceivablePaginationDTO> GetReceivablesAsync(int pageNumber = 1, int pageSize = 5)
        {

            var result = await _baseRepository.GetReceivablesAsync(pageNumber, pageSize);

            return new ReceivablePaginationDTO
            {
                Receivables = result.Entities.Select(x => new ReceivableDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    CostCenterId = x.CostCenterId,
                    FinancialGroupId = x.FinancialGroupId,
                    FinancialClassificationId = x.FinancialClassificationId,
                    CurrencyId = x.CurrencyId,
                    Type = (byte)x.Type,
                    Value = x.Value,
                    Discount = x.Discount,
                    TotalReceivable = x.TotalReceivable,
                    IssueDate = x.IssueDate,
                    DueDate = x.DueDate,
                    CashFlowDate = x.CashFlowDate,
                    AccrualPeriodDate = x.AccrualPeriodDate,
                    Observation = x.Observation,
                    Status = (byte)x.Status

                }).ToList(),
                CurrentPage = result.CurrentPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }
    }
}
