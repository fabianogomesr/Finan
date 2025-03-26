using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public  class CurrencyPaginationDTO : PaginationDTO
    {
        public List<CurrencyDTO>? Currencies { get; set; }
    }
}
