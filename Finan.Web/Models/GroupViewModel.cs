using System.ComponentModel.DataAnnotations;

namespace Finan.Web.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "A descrição deve ter no mínimo 1 caracteres.")]
        public string? Description { get; set; } = null;

        [Range(0, byte.MaxValue, ErrorMessage = "A natureza deve ser informada.")]
        public byte NatureId { get; set; } = 0;
    }
}
