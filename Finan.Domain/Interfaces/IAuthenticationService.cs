using Finan.Domain.Entities;
using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    // Abstração para autenticação (separa responsabilidade de TokenService)
    public interface IAuthenticationService
    {
        Task<User?> AuthenticateAsync(LoginRequest loginParameter);
    }
}
