using Finan.Domain.Interfaces;
using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Application.Validators;
using Finan.Infra.JWT;
using Finan.Infra.Identity.JWT;

namespace Finan.Application.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IAuthenticationService authenticationService,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        }

        public async Task<string?> GenerateTokenAsync(LoginRequest loginCommand)
        {
            if (!Validate(loginCommand, new LoginValidator()))
                return null;

            var user = await _authenticationService.AuthenticateAsync(loginCommand);

            if (user == null)
            {
                return null;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
