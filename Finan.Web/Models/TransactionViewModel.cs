using System.ComponentModel.DataAnnotations;

namespace Finan.Web.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O centro de custo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O centro de custo deve ser informado")]
        public int CostCenterId { get; set; }

        [Required(ErrorMessage = "O grupo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O grupo deve ser informado")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "A classificação é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A classificação deve ser informada")]
        public int ClassificationId { get; set; }

        [Required(ErrorMessage = "A moeda é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A moeda deve ser informada")]
        public int CurrencyId { get; set; }

        public byte TypeId { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        [Range(1, byte.MaxValue, ErrorMessage = "O status deve ser informado")]
        public byte StatusId { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório")]
        public decimal Value { get; set; }

        public decimal Discount { get; set; }

        public decimal LateFee { get; set; }

        [Required(ErrorMessage = "A data de emissão é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "A data de fluxo é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime CashFlowDate { get; set; }

        [Required(ErrorMessage = "A data de competencia é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime AccrualPeriodDate { get; set; }

        [Required(ErrorMessage = "A descrição obrigatória")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre 3 e 500 caracteres")]
        public string? Description { get; set; }

        [StringLength(1000, ErrorMessage = "A observação pode ter no máximo 1000 caracteres")]
        public string? Observation { get; set; }
        public decimal? PaidValue { get; set; }
        public DateTime? PaidDate { get; set; }
        public int? AccountId { get; set; }
    }
}
