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
    public interface IPaymentService : IBaseService<Payment>
    {
        Task<PaymentDTO> AddPayment(PaymentCommand PaymentParameter);
        Task<PaymentDTO> GetPaymentByIdAsync(int id);
        Task<List<PaymentDTO>> GetPaymentsAsync();
        Task<PaymentPaginationDTO> GetPaymentsAsync(PaymentFilter filter);
        List<PaymentTypeDTO> GetTypeList();
        List<PaymentStatusDTO> GetStatusList();
        Task<PaymentDTO> UpdatePayment(PaymentCommand PaymentParameter);
        List<DateTypeDTO> GetDateTypeList();
        Task<PaymentSummaryDTO> GetPaymentSummaryByMonthYear(int month, int year);
        Task<List<PaymentSummaryClassificationDTO>> GetPaymentSummaryClassificationByMonthYear(int month, int year);
    }
}
