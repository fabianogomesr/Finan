using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class AccountDeposit : BaseEntity
    {
        public CostCenter? CostCenter { get; set; }
        public FinancialGroup? FinancialGroup { get; set; }
        public FinancialClassification? FinancialClassification { get; set; }
        public AccountDepositType Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public DateTime AccrualPeriodDate { get; set; } // Data de competência
        public string? Observation { get; set; }
        public Receivable? Receivable { get; set; }
        public List<Statement>? Statements { get; set; }
    }
}
