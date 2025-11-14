using Finan.Domain.Parameters;
using FluentValidation;

namespace Finan.Service.Validators
{
    public class GroupValidator : AbstractValidator<GroupCommand>
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
