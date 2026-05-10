using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using FluentValidation;

namespace Finan.Application.Validators
{
    public class GroupValidator : AbstractValidator<GroupRequest>
    {
        public GroupValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.NatureId.GetHashCode())
            .Must(x => x == 0 || x == 1).WithMessage("A natureza tem que ser 0(Saida), 1(Entrada).");
        }
    }

}
