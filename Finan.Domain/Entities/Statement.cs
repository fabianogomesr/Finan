using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Statement : BaseEntity
    {
        public DateTime FlowDate { get; set; }
        public DateTime ReconciledDate { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public Payment? Payment { get; set; }
        public Receivable? Receivable { get; set; }
        public AccountDeposit? AccountDeposit { get; set; }
        public Account? Account { get; set; }
        public bool Reversed { get; set; }
    }
}
