using Finan.Domain.Parameters;

namespace Finan.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(LoginCommand loginParameter);
    }
}
