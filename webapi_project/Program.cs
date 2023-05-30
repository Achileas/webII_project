using System;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "Note App", Description = "Take your note in markdown format", Version = "v1" }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Note App v1");
});

app.MapGet("/", () => "Hello, world");
app.MapGet("/note", () =>
{
    return new[] { new Note("Note 1"), new Note("Note 2"), new Note("Note 3") };
});

app.MapPost("/note", (Note note) =>
{
    System.Console.WriteLine("{0}", note);
});


app.Run();

public record Note(string note);