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
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _baseRepository;

        public PaymentService(IPaymentRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<PaymentDTO> AddPayment(PaymentCommand PaymentParameter)
        {
            Payment Payment = new Payment
            {
                CostCenterId = PaymentParameter.CostCenterId,
                FinancialGroupId = PaymentParameter.FinancialGroupId,
                FinancialClassificationId = PaymentParameter.FinancialClassificationId,
                CurrencyId = PaymentParameter.CurrencyId,
                Description = PaymentParameter.Description,
                Type = (Domain.Enums.PaymentTypeEnum)PaymentParameter.TypeId,
                Value = PaymentParameter.Value,
                Discount = PaymentParameter.Discount,
                LatePayments = PaymentParameter.LatePayments,
                TotalPaid = PaymentParameter.TotalPaid,
                IssueDate = PaymentParameter.IssueDate,
                DueDate = PaymentParameter.DueDate,
                CashFlowDate = PaymentParameter.CashFlowDate,
                AccrualPeriodDate = PaymentParameter.AccrualPeriodDate,
                PayerId = PaymentParameter.PayerId,
                Observation = PaymentParameter.Observation,
                Status = (PaymentStatus)PaymentParameter.StatusId
            };

            await _baseRepository.Insert(Payment);

            return new PaymentDTO
            {
                Id = Payment.Id,
                CostCenterId = Payment.CostCenterId,
                FinancialGroupId = Payment.FinancialGroupId,
                FinancialClassificationId = Payment.FinancialClassificationId,
                CurrencyId = Payment.CurrencyId,
                TypeId = (byte)Payment.Type.GetHashCode(),
                Value = Payment.Value,
                Discount = Payment.Discount,
                LatePayments = Payment.LatePayments,
                TotalPaid = Payment.TotalPaid,
                IssueDate = Payment.IssueDate,
                DueDate = Payment.DueDate,
                CashFlowDate = Payment.CashFlowDate,
                AccrualPeriodDate = Payment.AccrualPeriodDate,
                PayerId = Payment.PayerId,
                Observation = Payment.Observation,
                StatusId = (byte)Payment.Status.GetHashCode()
            };
        }

        public async Task<List<PaymentDTO>> GetPaymentsAsync()
        {
            var result = await _baseRepository.GetPaymentsAsync();

            if (result == null)
                return null;

            return result.Select(x => new PaymentDTO
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                FinancialGroupId = x.FinancialGroupId,
                FinancialClassificationId = x.FinancialClassificationId,
                CurrencyId = x.CurrencyId,
                TypeId = (byte)x.Type.GetHashCode(),
                Value = x.Value,
                Discount = x.Discount,
                LatePayments = x.LatePayments,
                TotalPaid = x.TotalPaid,
                IssueDate = x.IssueDate,
                DueDate = x.DueDate,
                CashFlowDate = x.CashFlowDate,
                AccrualPeriodDate = x.AccrualPeriodDate,
                PayerId = x.PayerId,
                Observation = x.Observation,
                StatusId = (byte)x.Status.GetHashCode()
            }).ToList();
        }

        public async Task<PaymentDTO> GetPaymentByIdAsync(int id)
        {
            var result = await _baseRepository.GetPaymentByIdAsync(id);

            if (result == null)
                return null;

            return new PaymentDTO
            {
                Id = result.Id,
                CostCenterId = result.CostCenterId,
                FinancialGroupId = result.FinancialGroupId,
                FinancialClassificationId = result.FinancialClassificationId,
                CurrencyId = result.CurrencyId,
                TypeId = (byte)result.Type.GetHashCode(),
                Description = result.Description,
                Value = result.Value,
                Discount = result.Discount,
                LatePayments = result.LatePayments,
                TotalPaid = result.TotalPaid,
                IssueDate = result.IssueDate,
                DueDate = result.DueDate,
                CashFlowDate = result.CashFlowDate,
                AccrualPeriodDate = result.AccrualPeriodDate,
                PayerId = result.PayerId,
                Observation = result.Observation,
                StatusId = (byte)result.Status.GetHashCode()
            };
        }

        public async Task<PaymentDTO> UpdatePayment(PaymentCommand PaymentParameter) 
        {
            var Payment = _baseRepository.Select(PaymentParameter.Id).Result;

            if (Payment == null)
                throw new Exception("Pagamento não localizado.");

            Payment.CostCenterId = PaymentParameter.CostCenterId;
            Payment.FinancialGroupId = PaymentParameter.FinancialGroupId;
            Payment.FinancialClassificationId = PaymentParameter.FinancialClassificationId;
            Payment.CurrencyId = PaymentParameter.CurrencyId;
            Payment.Type = (Domain.Enums.PaymentTypeEnum)PaymentParameter.TypeId;
            Payment.Description = PaymentParameter.Description;
            Payment.Value = PaymentParameter.Value;
            Payment.Discount = PaymentParameter.Discount;
            Payment.LatePayments = PaymentParameter.LatePayments;
            Payment.TotalPaid = PaymentParameter.TotalPaid;
            Payment.IssueDate = PaymentParameter.IssueDate;
            Payment.DueDate = PaymentParameter.DueDate;
            Payment.CashFlowDate = PaymentParameter.CashFlowDate;
            Payment.AccrualPeriodDate = PaymentParameter.AccrualPeriodDate;
            Payment.PayerId = PaymentParameter.PayerId;
            Payment.Observation = PaymentParameter.Observation;
            Payment.Status = (PaymentStatus)PaymentParameter.StatusId;

            await _baseRepository.Update(Payment);

            return new PaymentDTO
            {
                Id = Payment.Id,
                CostCenterId = Payment.CostCenterId,
                FinancialGroupId = Payment.FinancialGroupId,
                FinancialClassificationId = Payment.FinancialClassificationId,
                CurrencyId = Payment.CurrencyId,
                TypeId = (byte)Payment.Type.GetHashCode(),
                Value = Payment.Value,
                Discount = Payment.Discount,
                LatePayments = Payment.LatePayments,
                TotalPaid = Payment.TotalPaid,
                IssueDate = Payment.IssueDate,
                DueDate = Payment.DueDate,
                CashFlowDate = Payment.CashFlowDate,
                AccrualPeriodDate = Payment.AccrualPeriodDate,
                PayerId = Payment.PayerId,
                Observation = Payment.Observation,
                StatusId = (byte)Payment.Status.GetHashCode()
            };
        }

        public async Task<PaymentPaginationDTO> GetPaymentsAsync(PaymentFilter filter)
        {
            var result = await _baseRepository.GetPaymentsAsync(filter);

            return new PaymentPaginationDTO
            {
                Payments = result.Entities.Select(x => new PaymentDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    CostCenterId = x.CostCenter != null ? x.CostCenter.Id : 0,
                    CostCenterName = x.CostCenter != null ? x.CostCenter.Description : string.Empty,
                    FinancialGroupId = x.FinancialGroup != null ? x.FinancialGroup.Id : 0,
                    FinancialGroupName = x.FinancialGroup != null ? x.FinancialGroup.Description : String.Empty,
                    FinancialClassificationId = x.FinancialClassification != null ? x.FinancialClassification.Id : 0,
                    FinancialClassificationName = x.FinancialClassification != null ? x.FinancialClassification.Description : String.Empty,
                    CurrencyId = x.CurrencyId,
                    CurrencyName = x.Currency != null ? x.Currency.Code : String.Empty,
                    TypeId = (byte)x.Type,
                    TypeName = x.Type.GetDescription(),
                    Value = x.Value,
                    Discount = x.Discount,
                    LatePayments = x.LatePayments,
                    TotalPaid = x.TotalPaid,
                    IssueDate = x.IssueDate,
                    DueDate = x.DueDate,
                    CashFlowDate = x.CashFlowDate,
                    AccrualPeriodDate = x.AccrualPeriodDate,
                    PayerId = x.PayerId,
                    Observation = x.Observation,
                    StatusId = (byte)x.Status,
                    StatusName = x.Status.GetDescription()

                }).ToList(),
                CurrentPage = result.CurrentPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }

        public List<PaymentTypeDTO> GetTypeList()
        {
            var result = EnumExtensions.GetEnumList<PaymentTypeEnum>();

            return result.Select(x => new PaymentTypeDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }

        public List<PaymentStatusDTO> GetStatusList()
        {
            var result = EnumExtensions.GetEnumList<PaymentStatus>();

            return result.Select(x => new PaymentStatusDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }
    }
}

