using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;

namespace Finan.Domain.Interfaces
{
    public interface IAuthService : IBaseService
    {
        Task<string?> GenerateTokenAsync(LoginRequest loginCommand);
    }
}
