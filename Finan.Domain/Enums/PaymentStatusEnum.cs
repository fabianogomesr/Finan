using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum PaymentStatus : byte
    {
        Open = 1,
        Paid = 2,
        Canceled = 3
    }
}
