using CinemaMont.Dtos;
using CinemaMont.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ModelsContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("ModelsContext")));
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
     policy =>
     {
         policy.WithOrigins("http://localhost:5500",
         "http://127.0.0.1:5500")
         .AllowAnyHeader()
         .AllowAnyMethod();
     });
});


var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Swagggggger";
    });
}

app.MapGet("/movies", async (ModelsContext db) =>
{
    var movies = await db.Movies
                        .Select(m => new MovieDto
                        (m.MovieId, m.MovieTitle, m.DateBroadcast, m.Time, m.Genre))
                        .ToListAsync();
    return Results.Ok(movies);
});

app.MapPost("/movies", async (ModelsContext db, CreateMovieDto dto) =>
{
    var movie = new Movie
    {
        MovieTitle = dto.Title,
        DateBroadcast = dto.Date,
        Time = dto.Time,
        Genre = dto.Genre
    };

    db.Movies.Add(movie);
    await db.SaveChangesAsync();
    return Results.Created($"/movies/{movie.MovieId}",
        new MovieDto(movie.MovieId, movie.MovieTitle, movie.DateBroadcast, movie.Time, movie.Genre));
});

app.MapGet("/movies/{id}", async (ModelsContext db, int id) =>
{
    var movie = await db.Movies.FindAsync(id);
    return Results.Ok(movie);
});

app.MapDelete("movies/{id}", async (ModelsContext db, int id) =>
{
    var movie = await db.Movies.FindAsync(id);
    if (movie is null)
    {
        return Results.NotFound();
    }
    else
    {
        db.Movies.Remove(movie);
        await db.SaveChangesAsync();
        return Results.Ok($"Success! You deleted an item with ID: {id}");
    }
});

app.MapPost("/register", async (IPasswordHasher<User> hasher, ModelsContext db, CreateUserDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
    {
        return Results.BadRequest("Username and Password fields are must have!");
    }

    var possible = await db.Users.AnyAsync(u => u.Username == dto.Username);
    if (possible)
    {
        return Results.BadRequest("This username already exists!");
    }

    var user = new User
    {
        Username = dto.Username,
        Type = UserType.BASIC
    };
    user.Password = hasher.HashPassword(user, dto.Password);

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.UserId}",
        new UsersDto(user.UserId, user.Type, user.Username));
});

app.MapPost("/login", async (IPasswordHasher<User> hasher, ModelsContext db, LoginDto dto) =>
{
    var user = await db.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
    if(user is null)
    {
        return Results.BadRequest("This user doesn't exist!");
    }

    var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);
    if(result == PasswordVerificationResult.Failed)
    {
        return Results.Unauthorized();
    }
    
    return Results.Ok("BRAVO PAKI, ULOGOVAN SI");
});

app.Run();
