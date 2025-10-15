using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public abstract class BaseContractEntity : BaseEntity
    {
        public virtual int ContractId { get; set; }
    }
}
