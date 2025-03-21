using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Bank : BaseEntity
    {
        public string? Name { get; set; }
        public string? Code { get; set; }

        public List<Account>? Accounts { get; set; }
    }
}
