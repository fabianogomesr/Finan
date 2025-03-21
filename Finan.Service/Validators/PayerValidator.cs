using Finan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Validators
{
    public class PayerValidator : AbstractValidator<Payer>
    {
        public PayerValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 50).WithMessage("O nome deve ter entre 1 e 50 caracteres.");
        }
    }   
}
