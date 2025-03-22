using Finan.Domain.Entities;
using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Commands
{
    public class ReceivableCommand
    {
        public int Id { get; set; }
        public int CostCenterId { get; set; }
        public int FinancialGroupId { get; set; }
        public int FinancialClassificationId { get; set; }
        public int CurrencyId { get; set; }
        public string? Description { get; set; }
        public byte Type { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalReceivable { get; set; }
        public DateTime IssueDate { get; set; } //Data de Emissão
        public DateTime DueDate { get; set; } //Data de Vencimento
        public DateTime CashFlowDate { get; set; } //Data de Fluxo 
        public DateTime AccrualPeriodDate { get; set; } //Data de Competencia
        public string? Observation { get; set; }
        public byte Status { get; set; }
    }
}
