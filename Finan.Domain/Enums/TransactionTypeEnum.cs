using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum TransactionType : byte
    {
        Confirmed = 1, //Confirmado
        Projected = 2, //Orçamento
        Anticipated = 3 //Previsto
    }
}
