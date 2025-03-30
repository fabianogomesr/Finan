using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class PayerPaginationDTO : PaginationDTO
    {
        public List<PayerDTO>? Payers { get; set; }
    }
}
