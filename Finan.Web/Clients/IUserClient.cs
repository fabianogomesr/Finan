
using Finan.Contracts.Request;
using Finan.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Web.Clients
{
    public interface IUserClient
    {
        Task<string> GetTokenAsync(string userName, string pass);
        Task<UserResponse> CreateAdminUserAsync(UserRequest user);
    }
}
