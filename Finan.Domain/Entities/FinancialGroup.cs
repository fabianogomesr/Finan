using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class FinancialGroup : BaseEntity
    {
        public string? Description { get; set; }

        public List<FinancialClassification> FinancialClassifications { get; set; }

        public List<AccountDeposit>? AccountDeposits { get; set; }
        public List<Receivable>? Receivables { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
