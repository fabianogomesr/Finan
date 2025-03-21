using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Payer : BaseEntity
    {
        public string? Name { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
