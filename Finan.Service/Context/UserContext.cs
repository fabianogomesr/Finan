using Finan.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Finan.Service.Context
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
            LoadUser();
        }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int ContractId { get; set; }

        private void LoadUser()
        {
            if(_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("login", out var login) && !string.IsNullOrEmpty(login))
            {
                UserName = login;
                
            }

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("role", out var role) && !string.IsNullOrEmpty(role))
            {
                Role = role;
            }

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("contractId", out var contractId) && !string.IsNullOrEmpty(contractId) && int.TryParse(contractId, out var contractIdInt))
            {
                ContractId = contractIdInt;
            }
        }
    }
}
