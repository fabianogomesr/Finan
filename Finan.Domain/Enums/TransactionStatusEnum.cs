using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum TransactionStatus : byte
    {
        [Description("Aberto")]
        Open = 1,
        [Description("Pago")]
        Paid = 2,
        [Description("Cancelado")]
        Canceled = 3
    }
}
