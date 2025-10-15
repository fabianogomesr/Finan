using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class AuthDTO
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public int ContractId { get; set; }
    }
}
