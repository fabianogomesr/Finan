using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using FluentValidation;

namespace Finan.Application.Validators
{
    public class AccountValidator : AbstractValidator<AccountRequest>
    {
        public AccountValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Agency)
            .NotEmpty().WithMessage("O agência é obrigatória.")
            .Length(1, 10).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Number)
            .NotEmpty().WithMessage("O número da conta é obrigatório.")
            .Length(1, 15).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.CreditLimit)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.")
            .ScalePrecision(2, 18).WithMessage("O valor deve ter no máximo 2 casas decimais e até 18 dígitos totais.");

            RuleFor(p => p.BankId)
            .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");
        }
    }
}
