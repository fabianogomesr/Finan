using System.ComponentModel;

namespace Finan.Domain.Enums
{
    public enum TransactionType : byte
    {
        [Description("Despesa")]
        Expense = 0,
        [Description("Receita")]
        Income = 1
    }
}
