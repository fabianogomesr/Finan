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
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<List<Payment>> GetPaymentsAsync();
        Task<EntityPagination<Payment>> GetPaymentsAsync(PaymentFilter filter);
        Task<PaymentSummaryDTO> GetPaymentSummaryByMonthYear(int month, int year);
        Task<List<PaymentSummaryClassificationDTO>> GetPaymentSummaryClassificationByMonthYear(int month, int year);
    }
}
