using CounterTask.Data;
using CounterTask.Hubs;
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
services.AddSignalR();
services.AddControllers();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapHub<CounterHub>("/counterhub");
app.MapControllers();

app.Run();
