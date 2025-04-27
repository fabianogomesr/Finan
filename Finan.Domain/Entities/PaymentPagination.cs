using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class PaymentPagination : Pagination
    {
        public List<Payment>? Payments { get; set; }
    }
}
