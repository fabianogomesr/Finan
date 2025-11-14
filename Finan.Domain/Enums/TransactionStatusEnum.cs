using System.ComponentModel;

namespace Finan.Domain.Enums
{
    public enum TransactionStatus
    {
        [Description("Aberto")]
        Open = 1,
        [Description("Pago")]
        Paid = 2,
        [Description("Cancelado")]
        Canceled = 3
    }
}
