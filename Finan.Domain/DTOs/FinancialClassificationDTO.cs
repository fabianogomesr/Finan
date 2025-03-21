using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class FinancialClassificationDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public int FinancialGroupId { get; set; }
    }
}
