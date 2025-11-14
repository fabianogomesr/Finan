using Finan.CrossCutting.Encrypt;
using Finan.Domain.DTOs;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Mappers;
using Finan.Service.Validators;

namespace Finan.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _baseRepository;
        private readonly ISeedDataRepository _seedDataRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository baseRepository,
            ISeedDataRepository seedDataRepository,
            IPasswordHasher passwordHasher)
        {
            _baseRepository = baseRepository;
            _seedDataRepository = seedDataRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDTO?> CreateUser(UserCommand userCommand)
        {
            if (!Validate(userCommand, new UserValidator()))
                return null;

            var user = UserMap.CommandToEntity(userCommand);

            user.Password = _passwordHasher.Hash(user.Password!);

            await _baseRepository.Insert(user);

            await _seedDataRepository.SeedDefaultDataAsync(user.TenantId);

            return UserMap.EntityToDto(user);
        }

        public async Task<UserDTO?> GetByUserNameAsync(string userName) 
        {
            var result = await _baseRepository.GetUserByUserName(userName);

            if (result == null)
                return null;

            return UserMap.EntityToDto(result);
        }

        public async Task<UserDTO?> UpdateUser(UserCommand userCommand)
        {
            if (Validate(userCommand, new UserValidator()))
                return null;

            var user = await _baseRepository.GetUserByUserName(userCommand.UserName!);

            if (user == null)
            {
                Messages.Error("Usuário não encontrado.");
                return null;
            }

            user.Password = _passwordHasher.Hash(user.Password!);

            await _baseRepository.Insert(user);

            return UserMap.EntityToDto(user);
        }
    }
}
