var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var sevices = builder.Services;

sevices.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();