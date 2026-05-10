using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using FluentValidation;

namespace Finan.Application.Validators
{
    public class CostCenterValidator : AbstractValidator<CostCenterRequest>
    {
        public CostCenterValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");
        }
    }
}
