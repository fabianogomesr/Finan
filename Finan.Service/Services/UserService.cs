using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _baseRepository;

        public UserService(IUserRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<User> GetByUserNameAsync(string userName) =>
            await _baseRepository.GetUserByUserName(userName);
    }
}
