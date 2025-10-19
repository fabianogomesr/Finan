using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public abstract class MultiTenantEntity : BaseEntity
    {
        public virtual Guid TenantId { get; set; } // ou string, dependendo da sua modelagem
    }
}
