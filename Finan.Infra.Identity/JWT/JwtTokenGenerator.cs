using Finan.Domain.Entities;
using Finan.Infra.Identity.JWT;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finan.Infra.JWT
{
    // Implementação concreta da geração de token
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _options;
        private readonly byte[] _keyBytes;

        public JwtTokenGenerator(IOptions<JwtOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _keyBytes = Encoding.UTF8.GetBytes(_options.Key ?? string.Empty);
            if (_keyBytes.Length == 0) throw new ArgumentException("Jwt key não configurada.");
        }

        public string GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(_keyBytes), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim("tenant_id", user.TenantId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_options.ExpireHours),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
