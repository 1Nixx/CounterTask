using CounterTask.Data;
using CounterTask.Interfaces;
using CounterTask.Middleware;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddSingleton(typeof(CounterContext));
services.AddScoped<ICounterRepository, CounterRepository>();
services.AddCors(opt =>
	{
		opt.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
	});
services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
