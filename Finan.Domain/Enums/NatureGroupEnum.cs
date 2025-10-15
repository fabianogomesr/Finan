using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum NatureGroup : byte
    {
        [Description("Saida")]
        Debit = 0,
        [Description("Entrada")]
        Credit = 1
    }
}