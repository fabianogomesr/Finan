using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Filters
{
    public class BaseFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
