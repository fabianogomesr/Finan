using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class CostCenterPaginationDTO : PaginationDTO
    {
        public List<CostCenterDTO> CostCenters { get; set; }
    }
}
