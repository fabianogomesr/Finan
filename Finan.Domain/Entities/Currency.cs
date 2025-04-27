using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Currency : BaseEntity
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Symbol { get; set; }

        public List<Receivable>? Receivables { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
