using System.ComponentModel.DataAnnotations;

namespace Finan.Web.Models
{
    public class BankViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter no mínimo 1 caracteres.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A código é obrigatório.")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "O agência deve ter no mínimo 1 caracteres.")]
        public string? Code { get; set; }



    }
}
