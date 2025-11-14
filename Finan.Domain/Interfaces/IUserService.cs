using Finan.Domain.DTOs;
using Finan.Domain.Parameters;

namespace Finan.Domain.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<UserDTO?> GetByUserNameAsync(string email);
        Task<UserDTO?> CreateUser(UserCommand userCommand);
        Task<UserDTO?> UpdateUser(UserCommand userCommand);
    }
}
