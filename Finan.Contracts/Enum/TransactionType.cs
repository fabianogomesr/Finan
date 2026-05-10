using System.ComponentModel;

namespace Finan.Contracts.Enums
{
    public enum TransactionType : byte
    {
        [Description("Despesa")]
        Expense = 0,
        [Description("Receita")]
        Income = 1
    }
}
