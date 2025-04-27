using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int? CostCenterId { get; set; }
        public string? CostCenterName { get; set; }
        public int? FinancialGroupId { get; set; }
        public string? FinancialGroupName { get; set; }
        public int? FinancialClassificationId { get; set; }
        public string? FinancialClassificationName { get; set; }
        public int? CurrencyId { get; set; }
        public string? CurrencyName { get; set; }
        public string? Description { get; set; }
        public byte TypeId { get; set; }
        public string? TypeName { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal LatePayments { get; set; }
        public decimal TotalPaid { get; set; }
        public DateTime IssueDate { get; set; } //Data de Emissão
        public DateTime DueDate { get; set; } //Data de Vencimento
        public DateTime CashFlowDate { get; set; } //Data de Fluxo 
        public DateTime AccrualPeriodDate { get; set; } //Data de Competencia
        public string? Observation { get; set; }
        public byte StatusId { get; set; }
        public string? StatusName { get; set; }
        public int PayerId { get; set; }
    }
}
