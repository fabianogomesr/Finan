using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration; 
        private readonly IUserRepository _userRepository;

        public TokenService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public async Task<string> GenerateTokenAsync(LoginCommand loginParameter)
        {
            var userDataBase = await _userRepository.GetUserByUserName(loginParameter.UserName);

            if(userDataBase == null)
            {
                return string.Empty;
            }

            if(loginParameter.UserName != userDataBase.UserName || loginParameter.Password != userDataBase.Password)
            {
                return string.Empty;
            }
             
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: new[]
            {
                new Claim(type: ClaimTypes.Name, userDataBase.UserName),
                new Claim("tenant_id", userDataBase.TenantId.ToString())
            },

            expires: DateTime.Now.AddHours(2),
            signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}
