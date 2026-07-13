using Microsoft.EntityFrameworkCore;


namespace CinemaMont.Models
{
    public class ModelsContext : DbContext
    {
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }

    public enum UserType
    {
        BASIC,
        OWNER,
        ADMIN
    }

    public enum Genre
    {
        HORROR,
        ACTION,
        SCIFI,
        DOCUMENTARY,
        THRILLER
    }

    public class User
    {
        public UserType Type { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
    }

    public class Room
    {
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public int NumberOfSeats { get; set; }
    }

    public class Movie
    {
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public DateTime? DateBroadcast { get; set; }
        public TimeOnly Time { get; set; }
        public Genre Genre { get; set; }
    }

    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public int NumberReserved { get; set; }
    }
}
