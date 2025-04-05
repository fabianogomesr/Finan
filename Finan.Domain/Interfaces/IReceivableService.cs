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
    public interface IReceivableService : IBaseService<Receivable>
    {
        Task<ReceivableDTO?> AddReceivable<ReceivableValidator>(ReceivableCommand receivableParameter);
        Task<ReceivableDTO> GetReceivableByIdAsync(int id);
        Task<List<ReceivableDTO>> GetReceivablesAsync();
        Task<ReceivablePaginationDTO> GetReceivablesAsync(int pageNumber = 1, int pageSize = 5);
        Task<ReceivableDTO?> UpdateReceivable<ReceivableValidator>(ReceivableCommand receivableParameter);
    }
}
