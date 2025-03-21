using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum TransactionStatus : byte
    {
        Open = 1,
        ReceivedOrPaid = 2,
        Canceled = 3
    }
}
