using Serilog;
using SerilogLogging.Middlewares;
using static System.Net.WebRequestMethods;

namespace SerilogLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration().
                MinimumLevel.Information().
                WriteTo.Console().
                WriteTo.File("logs/logs-.txt", rollingInterval: RollingInterval.Day).
                CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseSerilog((context, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddApplicationInsightsTelemetry();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRequestContextLogging();

            app.UseSerilogRequestLogging(); // Logs Every Request eg. HTTP GET / WeatherForecast responded 200 in 202.6869 ms

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}