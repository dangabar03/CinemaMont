using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace CinemaMont.Models
{
    public class ModelsContext : DbContext
    {
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
            { 
                u.Property(x => x.Username).IsRequired();
                u.HasData(
                    new User { Type = UserType.BASIC, UserId = 1, Username = "Aleksandar" },
                    new User { Type = UserType.OWNER, UserId = 2, Username = "Vasilije" },
                    new User { Type = UserType.ADMIN, UserId = 3, Username = "Ivana" }
                    );
            }
            );

        }

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
        public int UserId { get; set; }
        public UserType Type { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
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
        public DateOnly DateBroadcast { get; set; }
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
