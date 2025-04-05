using Finan.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class ReceivablePagination : Pagination
    {
        public List<Receivable>? Receivables { get; set; }
    }
}
