using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class BankPagination :  Pagination
    {
        public List<Bank> Banks { get; set; }
    }
}
