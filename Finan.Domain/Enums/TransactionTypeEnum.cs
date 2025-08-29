using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
