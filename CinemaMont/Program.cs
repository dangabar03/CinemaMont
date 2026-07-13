using CinemaMont.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ModelsContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("ModelsContext")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Swagggggger";
    });
}


app.MapGet("/", () => "CINEMA");

app.MapGet("/swagger-works", () => "SWAGGER WORKS BABY boy");

app.Run();
