using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Contract : BaseEntity
    {
        public SubscriptionPlan? SubscriptionPlan { get; set; }
        public int SubscriptionPlanId { get; set; }
        public List<User>? Users { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
