using Finan.Domain.Commands;
using FluentValidation;

namespace Finan.Service.Validators
{
    public class BankValidator : AbstractValidator<BankCommand>
    {
        public BankValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Code)
            .NotEmpty().WithMessage("O código é obrigatório.")
            .Length(1, 5).WithMessage("O nome deve ter entre 1 e 5 caracteres.");
        }
    }
}
