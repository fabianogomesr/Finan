using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Parameters
{
    public class GroupCommand
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public NatureGroup NatureId { get; set; }
    }
}
