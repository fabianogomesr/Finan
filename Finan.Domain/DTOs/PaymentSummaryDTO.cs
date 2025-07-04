using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class PaymentSummaryDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
