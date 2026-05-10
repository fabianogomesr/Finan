using System.ComponentModel.DataAnnotations;

namespace Finan.Web.Models
{
    public class CostCenterViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "A descrição deve ter no mínimo 1 caracteres.")]
        public string? Description { get; set; }
    }
}
