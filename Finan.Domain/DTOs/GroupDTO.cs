using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Nature { get; set; }
        public byte NatureId { get; set; }
    }
}
