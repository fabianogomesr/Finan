using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Validators;
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
        private ISeedDataRepository _seedDataRepository;

        public UserService(IUserRepository baseRepository, ISeedDataRepository seedDataRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _seedDataRepository = seedDataRepository;
        }

        public async Task<User> CreateUser(UserCommand userCommand)
        {
            var user = new User
            {
                UserName = userCommand.UserName,
                Password = userCommand.Password,
                Email = userCommand.Email,
                TenantId = Guid.NewGuid()
            };

            Validate(user, new UserValidator());

            await _baseRepository.Insert(user);

            await _seedDataRepository.SeedDefaultDataAsync(user.TenantId);

            return user;
        }

        public async Task<User> GetByUserNameAsync(string userName) =>
            await _baseRepository.GetUserByUserName(userName);
    }
}
