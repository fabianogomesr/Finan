using Finan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Validators
{
    public class ContractValidator : AbstractValidator<Contract>
    {
        public ContractValidator()
        {
            RuleFor(c => c.SubscriptionPlanId)
                .NotEmpty().WithMessage("O plano é obrigatório.")
                .NotNull().WithMessage("O plano é obrigatório.");

            RuleFor(c => c.StartDate)
                .NotEmpty().WithMessage("O período é obrigatório.")
                .NotNull().WithMessage("O período é obrigatório.");

            RuleFor(c => c.EndDate)
                .NotEmpty().WithMessage("O período é obrigatório.")
                .NotNull().WithMessage("O período é obrigatório.");
        }
    }
}
