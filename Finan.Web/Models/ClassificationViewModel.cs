using System.ComponentModel.DataAnnotations;

namespace Finan.Web.Models
{
    public class ClassificationViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "A descrição deve ter no mínimo 1 caracteres.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "O grupo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O grupo deve ser informado")]
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
