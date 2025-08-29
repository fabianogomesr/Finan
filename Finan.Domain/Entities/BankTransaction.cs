using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class BankTransaction : BaseEntity
    {
        public CostCenter? CostCenter { get; set; }
        public Group? Group { get; set; }
        public Classification? Classification { get; set; }
        public BankTransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public DateTime AccrualPeriodDate { get; set; } // Data de competência
        public string? Observation { get; set; }
        public List<Statement>? Statements { get; set; }
    }
}
