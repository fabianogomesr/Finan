using System.ComponentModel;

namespace Finan.Contracts.Enums
{
    public enum NatureGroup : byte
    {
        [Description("Saida")]
        Debit = 0,
        [Description("Entrada")]
        Credit = 1
    }
}