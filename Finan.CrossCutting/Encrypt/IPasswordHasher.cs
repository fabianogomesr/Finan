using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.CrossCutting.Encrypt
{
    // Abstração para validação/geração de hash de senha
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashed);
    }
}
