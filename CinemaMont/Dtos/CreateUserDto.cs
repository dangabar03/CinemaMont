using CinemaMont.Models;

namespace CinemaMont.Dtos
{
    public record CreateUserDto(UserType Type, string Username, string Password);
}
