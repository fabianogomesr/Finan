using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class ReceivableSummaryClassificationDTO
    {
        public string? Classification { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
