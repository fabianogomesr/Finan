using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public CostCenter? CostCenter { get; set; }
        public FinancialGroup? FinancialGroup { get; set; }
        public FinancialClassification? FinancialClassification { get; set; }
        public Currency? Currency { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal LatePayments { get; set; } //Juros
        public decimal TotalPaid { get; set; }
        public DateTime IssueDate { get; set; } //Data de Emissão
        public DateTime DueDate { get; set; } //Data de Vencimento
        public DateTime CashFlowDate { get; set; } //Data de Fluxo 
        public DateTime AccrualPeriodDate { get; set; } //Data de Competencia
        public Payer? Payer { get; set; } //Data de Competencia
        public string? Observation { get; set; }
        public PaymentStatus Status { get; set; }
        public List<Statement>? Statements { get; set; }
    }
}
