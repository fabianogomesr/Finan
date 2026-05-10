using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;

namespace Finan.Domain.Interfaces
{
    public interface IUserService : IBaseService
    {
        Task<UserResponse?> GetByUserNameAsync(string email);
        Task<UserResponse?> CreateUser(UserRequest userCommand);
        Task<UserResponse?> UpdateUser(UserRequest userCommand);
    }
}
