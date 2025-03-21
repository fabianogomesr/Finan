using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class FinancialGroupPaginationDTO : PaginationDTO
    {
        public List<FinancialGroupDTO> FinancialGroups { get; set; }
    }
}
