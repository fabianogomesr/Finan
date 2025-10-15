using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Filters
{
    public class TransactionFilter : BaseFilter
    {
        public TransactionType TransactionType { get; set; }
        public DateType DateType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Canceled { get; set; }
    }
}
