using Finan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Validators
{
    public class FinancialGroupValidator : AbstractValidator<FinancialGroup>
    {
        public FinancialGroupValidator()
        {
            RuleFor(p => p.Description)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");
        }
    }

}
