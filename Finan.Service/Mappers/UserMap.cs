using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Parameters;

namespace Finan.Service.Mappers
{
    public class UserMap
    {
        public static UserDTO EntityToDto(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public static User CommandToEntity(UserCommand userCommand)
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
