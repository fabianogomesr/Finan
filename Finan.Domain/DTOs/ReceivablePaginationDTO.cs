using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class ReceivablePaginationDTO : PaginationDTO
    {
        public List<ReceivableDTO>? Receivables { get; set; }
    }
}
