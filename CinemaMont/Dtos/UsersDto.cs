using CinemaMont.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CinemaMont.Dtos
{
    public record UsersDto(int UserId, UserType Type, string Username);
}
