using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class ClassificationDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
