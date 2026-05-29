using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Contracts.Response
{
    public class ReceivableSummaryByClassificationResponse
    {
        public string ClassificationName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }
}
