using Finan.Contracts.Enums;

namespace Finan.Domain.Entities
{
    public class Transaction : MultiTenantEntity
    {
        public CostCenter? CostCenter { get; set; }
        public int CostCenterId { get; set; }
        public Group? Group { get; set; }
        public int GroupId { get; set; }
        public Classification? Classification { get; set; }
        public int ClassificationId { get; set; }
        public Currency? Currency { get; set; }
        public int CurrencyId { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal LateFee { get; set; } //Juros
        public decimal TotalPaid { get; set; }
        public DateTime IssueDate { get; set; } //Data de Emissão
        public DateTime DueDate { get; set; } //Data de Vencimento
        public DateTime CashFlowDate { get; set; } //Data de Fluxo 
        public DateTime AccrualPeriodDate { get; set; } //Data de Competencia
        public string? Observation { get; set; }
        public TransactionStatus Status { get; set; }
        public List<Statement>? Statements { get; set; }
    }
}
