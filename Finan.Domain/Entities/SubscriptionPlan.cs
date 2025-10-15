using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class SubscriptionPlan : BaseEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int UserQuantity { get; set; }
        public List<Contract>? Contracts { get; set; }
    }
}
