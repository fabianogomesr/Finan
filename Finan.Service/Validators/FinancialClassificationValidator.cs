using Finan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Validators
{
    public class FinancialClassificationValidator : AbstractValidator<FinancialClassification>
    {
        public FinancialClassificationValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Type.GetHashCode())
                .GreaterThan(0).WithMessage("O tipo deve ser um valor positivo.")
                .Must(x => x == 1 || x == 2 || x == 3).WithMessage("O tipo tem que ser 0(Despesa), 1(Receita) ou 2(ambos).");

            RuleFor(p => p.FinancialGroup.Id)
            .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");
        }
    }
}
