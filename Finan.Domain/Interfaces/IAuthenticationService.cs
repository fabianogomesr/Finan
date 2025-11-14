using Finan.Domain.Entities;
using Finan.Domain.Parameters;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    // Abstração para autenticação (separa responsabilidade de TokenService)
    public interface IAuthenticationService
    {
        Task<User?> AuthenticateAsync(LoginCommand loginParameter);
    }
}
