using Finan.Domain.Commands;
using FluentValidation;

namespace Finan.Service.Validators
{
    public class CostCenterValidator : AbstractValidator<CostCenterCommand>
    {
        public CostCenterValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");
        }
    }
}
