using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Jwt
{
    // Abstração para geração de token
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
