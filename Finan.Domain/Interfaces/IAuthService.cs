using Finan.Domain.Parameters;

namespace Finan.Domain.Interfaces
{
    public interface IAuthService : IBaseService
    {
        Task<string?> GenerateTokenAsync(LoginCommand loginCommand);
    }
}
