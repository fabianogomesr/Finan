using Finan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Validators
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

            RuleFor(p => p.Nature.GetHashCode())
            .GreaterThan(0).WithMessage("A natureza deve ser um valor positivo.")
            .Must(x => x == 1 || x == 2 || x == 3).WithMessage("A natureza tem que ser 0(Saida), 1(Entrada) ou 2(ambos).");
        }
    }

}
