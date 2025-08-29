using Finan.Domain.Entities;
using FluentValidation;
using System;

namespace Finan.Service.Validators
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            RuleFor(p => p.Value)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero.")
                .ScalePrecision(2, 18).WithMessage("O valor deve ter no máximo 2 casas decimais e até 18 dígitos totais.");

            RuleFor(p => p.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("O desconto não pode ser negativo.")
                .ScalePrecision(2, 18).WithMessage("O desconto deve ter no máximo 2 casas decimais e até 18 dígitos totais.");

            RuleFor(p => p.LateFee)
                .GreaterThanOrEqualTo(0).WithMessage("O total a receber não pode ser negativo.")
                .ScalePrecision(2, 18).WithMessage("O total a receber deve ter no máximo 2 casas decimais e até 18 dígitos totais.");

            RuleFor(p => p.TotalPaid)
                .GreaterThanOrEqualTo(0).WithMessage("O total a receber não pode ser negativo.")
                .ScalePrecision(2, 18).WithMessage("O total a receber deve ter no máximo 2 casas decimais e até 18 dígitos totais.");

            RuleFor(p => p.IssueDate)
                .NotEmpty().WithMessage("A data de emissão é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de emissão não pode ser no futuro.");

            RuleFor(p => p.DueDate)
                .NotEmpty().WithMessage("A data de vencimento é obrigatória.")
                .GreaterThanOrEqualTo(p => p.IssueDate).WithMessage("A data de vencimento deve ser posterior ou igual à data de emissão.");

            RuleFor(p => p.CashFlowDate)
                .NotEmpty().WithMessage("A data de fluxo é obrigatória.");

            RuleFor(p => p.AccrualPeriodDate)
                .NotEmpty().WithMessage("A data de competência é obrigatória.");

            RuleFor(p => p.Status)
                .IsInEnum().WithMessage("O status da transação é inválido.");

            RuleFor(p => p.Group.Id)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.Classification.Id)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.CostCenter.Id)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.Currency.Id)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");
        }
    }
}
