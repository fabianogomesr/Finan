using Finan.Domain.Parameters;
using FluentValidation;

namespace Finan.Service.Validators
{
    public class ClassificationValidator : AbstractValidator<ClassificationCommand>
    {
        public ClassificationValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.GroupId)
            .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");
        }
    }
}
