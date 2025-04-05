using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Enums
{
    public enum ReceivableStatus : byte
    {
        Open = 1,
        Received = 2,
        Canceled = 3
    }
}
