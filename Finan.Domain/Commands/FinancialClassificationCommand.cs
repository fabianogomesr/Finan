using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Parameters
{
    public class FinancialClassificationCommand
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public byte TypeId { get; set; }
        public int FinancialGroupId { get; set; }
    }
}
