using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class FinancialClassification : BaseEntity
    {
        public string? Description { get; set; }
        public ClassificationType Type { get; set; }
        public FinancialGroup? FinancialGroup { get; set; }
        public int FinancialGroupId { get; set; }
        public List<AccountDeposit>? AccountDeposits { get; set; }
        public List<Receivable>? Receivables { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
