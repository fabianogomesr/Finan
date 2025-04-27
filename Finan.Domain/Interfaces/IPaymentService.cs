using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IPaymentService : IBaseService<Payment>
    {
        Task<PaymentDTO> AddPayment<PaymentValidator>(PaymentCommand PaymentParameter);
        Task<PaymentDTO> GetPaymentByIdAsync(int id);
        Task<List<PaymentDTO>> GetPaymentsAsync();
        Task<PaymentPaginationDTO> GetPaymentsAsync(int pageNumber = 1, int pageSize = 5);
        List<PaymentTypeDTO> GetTypeList();
        List<PaymentStatusDTO> GetStatusList();
        Task<PaymentDTO> UpdatePayment<PaymentValidator>(PaymentCommand PaymentParameter);
    }
}
