using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using FluentValidation;

namespace Finan.Application.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyRequest>
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
