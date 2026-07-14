using CinemaMont.Models;

namespace CinemaMont.Dtos
{
    public record CreateMovieDto(string Title, DateOnly Date, TimeOnly Time, Genre Genre);
}
