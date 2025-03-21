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
        Task<ReceivableDTO?> AddReceivable(ReceivableCommand receivableParameter);
        Task<List<ReceivableDTO>> CreateReceivablesAsync(List<ReceivableCommand> receivables);
        Task<ReceivableDTO> GetReceivableByIdAsync(int id);
        Task<ReceivableDTO?> UpdateReceivable(ReceivableCommand receivableParameter);
    }
}
