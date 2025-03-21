using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Parameters
{
    public class LoginCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
