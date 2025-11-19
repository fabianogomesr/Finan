using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Jwt;
using Finan.Service.Validators;

namespace Finan.Service.Services
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

        public async Task<string?> GenerateTokenAsync(LoginCommand loginCommand)
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
