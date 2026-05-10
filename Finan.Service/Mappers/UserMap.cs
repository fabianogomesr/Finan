using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Application.Mappers
{
    public class UserMap
    {
        public static UserResponse EntityToDto(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public static User CommandToEntity(UserRequest userCommand)
        {
            return new User
            {
                UserName = userCommand.UserName,
                Password = userCommand.Password,
                Email = userCommand.Email,
                TenantId = Guid.NewGuid()
            };
        }

    }
}
