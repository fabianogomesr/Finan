using System;
using System.Transactions;
using Finan.Domain.Commands;
using FluentValidation;

namespace Finan.Service.Validators
{
    public class TransactionValidator : AbstractValidator<TransactionCommand>
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

            RuleFor(p => p.StatusId)
                .IsInEnum().WithMessage("O status da transação é inválido.");

            RuleFor(p => p.GroupId)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.ClassificationId)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.CostCenterId)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.CurrencyId)
                .GreaterThan(0).WithMessage("O ID deve ser um valor positivo.");

            RuleFor(p => p.TypeId.GetHashCode())
                .Must(x => x == 0 || x == 1).WithMessage("O tipo tem que ser 0(Despesa), 1(Receita).");

            RuleFor(p => p.PaidTransaction)
                .NotNull().WithMessage("As informações de pagamento são obrigatórias quando o status é Pago.")
                .When(p => p.StatusId.GetHashCode() == Finan.Domain.Enums.TransactionStatus.Paid.GetHashCode());

            //// Validações que só se aplicam quando o status é 2 (Pago)
            //RuleFor(p => p.PaidTransaction.PaidDate)
            //    .NotEmpty().WithMessage("A data de pagamento é obrigatória quando o status é Pago.")
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de pagamento não pode ser no futuro.")
            //    .When(p => p.StatusId.GetHashCode() == Finan.Domain.Enums.TransactionStatus.Paid.GetHashCode());

            //RuleFor(p => p.PaidTransaction.PaidValue)
            //    .NotNull().WithMessage("O valor pago é obrigatório quando o status é Pago.")
            //    .GreaterThan(0).WithMessage("O valor pago deve ser maior que zero.")
            //    .ScalePrecision(2, 18).WithMessage("O valor pago deve ter no máximo 2 casas decimais e até 18 dígitos totais.")
            //    .When(p => p.StatusId.GetHashCode() == Finan.Domain.Enums.TransactionStatus.Paid.GetHashCode());

            //RuleFor(p => p.PaidTransaction.AccountId)
            //    .GreaterThan(0).WithMessage("A conta corrente é obrigatória quando o status é Pago.")
            //    .When(p => p.StatusId.GetHashCode() == Finan.Domain.Enums.TransactionStatus.Paid.GetHashCode());
        }
    }
}
