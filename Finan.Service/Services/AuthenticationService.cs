using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.CrossCutting.Encrypt;
using Finan.Contracts.Request;

namespace Finan.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<User?> AuthenticateAsync(LoginRequest loginParameter)
        {
            if (loginParameter == null) return null;
            if (string.IsNullOrWhiteSpace(loginParameter.UserName) || string.IsNullOrWhiteSpace(loginParameter.Password))
                return null;

            var user = await _userRepository.GetUserByUserName(loginParameter.UserName);
            if (user == null) return null;

            // Assume que user.Password está armazenada como hash.
            if (!_passwordHasher.Verify(loginParameter.Password, user.Password ?? string.Empty))
                return null;

            return user;
        }
    }
}
