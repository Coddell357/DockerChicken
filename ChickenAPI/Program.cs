using Microsoft.EntityFrameworkCore;
using ChickenAPI.Model;

namespace ChickenAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FarmDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"))); //connection string from docker-compose.yml


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi(); //Keep native engine    

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (true)
            {
                app.MapOpenApi(); //generates /openapi/v1.json

                app.UseSwaggerUI(options =>
                {
                    //Tell Swagger UI to look at the native .NET OpenSpi endpoint
                    options.SwaggerEndpoint("/openapi/v1.json", "Chicken API v1");
                    options.RoutePrefix = "swagger";
                }); // Serves the interactive web UI
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
