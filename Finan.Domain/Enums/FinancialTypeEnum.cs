using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum FinancialType : byte
    {
        Expense = 0, //Despesa
        Income = 1, //Receita
        Both = 2 //Ambos
    }
}