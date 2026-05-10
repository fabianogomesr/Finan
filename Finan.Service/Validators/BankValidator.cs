using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using FluentValidation;

namespace Finan.Application.Validators
{
    public class BankValidator : AbstractValidator<BankRequest>
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
