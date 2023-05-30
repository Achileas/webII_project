var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => {});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("Cors value");

app.MapGet("/api/note", () => "Hello, world");


app.Run();
