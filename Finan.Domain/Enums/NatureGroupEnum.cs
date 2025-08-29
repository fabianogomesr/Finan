using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum NatureGroupEnum : byte
    {
        [Description("Saida")]
        Debit = 0,
        [Description("Entrada")]
        Credit = 1,
        [Description("Ambos")]
        Both = 2
    }
}