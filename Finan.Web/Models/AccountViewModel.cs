using System.ComponentModel.DataAnnotations;

namespace Finan.Web.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O banco é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O banco deve ser informado")]
        public int BankId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter no mínimo 1 caracteres.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A agência é obrigatória.")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "O agência deve ter no mínimo 1 caracteres.")]
        public string? Agency { get; set; }

        [Required(ErrorMessage = "O número da conta é obrigatório.")]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "O número da conta deve ter no mínimo 1 caracteres.")]
        public string? Number { get; set; }

        public decimal CreditLimit { get; set; }

    }
}
