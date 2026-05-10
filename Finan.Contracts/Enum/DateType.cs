using System.ComponentModel;

namespace Finan.Contracts.Enums
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
