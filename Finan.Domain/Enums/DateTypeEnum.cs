using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum DateType
    {
        [Description("Emissão")]
        Issue = 1,
        [Description("Vencimento")]
        Due = 2,
        [Description("Fluxo")]
        CashFlow = 3,
        [Description("Competência")]
        AccrualPeriod = 4
    }
}
