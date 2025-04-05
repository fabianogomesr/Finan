using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IReceivableRepository : IBaseRepository<Receivable>
    {
        Task<Receivable> GetReceivableByIdAsync(int id);
        Task<List<Receivable>> GetReceivablesAsync();
        Task<EntityPagination<Receivable>> GetReceivablesAsync(int pageNumber, int pageSize);
    }
}
