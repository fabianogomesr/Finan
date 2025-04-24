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
        [Description("Confirmado")]
        Confirmed = 1, 
        [Description("Orçamento")]
        Projected = 2,
        [Description("Previsto")]
        Anticipated = 3 
    }
}
