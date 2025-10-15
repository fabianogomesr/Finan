using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IUserRepository : IBaseContractRepository<User>
    {
        Task<User> GetUserByUserName(string userName);
    }
}
