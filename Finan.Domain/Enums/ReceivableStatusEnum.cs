using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum ReceivableStatus : byte
    {
        [Description("Aberto")]
        Open = 1,
        [Description("Recebido")]
        Received = 2,
        [Description("Cancelado")]
        Canceled = 3
    }
}
