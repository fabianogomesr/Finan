using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Repository
{
    public class UserRepository : BaseContractRepository<User>, IUserRepository
    {
        protected readonly BaseContext _dbSet;

        public UserRepository(BaseContext mySqlContext, IUserContext userContext) : base(mySqlContext, userContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<User> GetUserByUserName(string userName) => await _dbSet.Set<User>().Where(x => x.UserName == userName).FirstOrDefaultAsync();



    }
}
