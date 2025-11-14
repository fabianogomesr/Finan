using Finan.Domain.Commands;
using FluentValidation;

namespace Finan.Service.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyCommand>
    {
        public CurrencyValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("O code é obrigatório.")
                .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Symbol)
                .NotEmpty().WithMessage("O symbal é obrigatório.")
                .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");
        }
    }
}
