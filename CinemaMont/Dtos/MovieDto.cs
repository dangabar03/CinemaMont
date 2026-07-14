using CinemaMont.Models;

namespace CinemaMont.Dtos
{
    public record MovieDto(int Id, string? Title, DateOnly Date, TimeOnly Time, Genre Genre);
}
