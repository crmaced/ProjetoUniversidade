using Serilog;
using System.Security.Cryptography;
using Universidade.Api.CrossCutting.Injections;

namespace Universidade.Api;
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder
                                .Configuration
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables()
                                .Build();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.InjectConfigs(Configuration);
            builder.Services.ConfigureServices(Configuration);
            builder.Services.InjectRepositories(Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(policy => policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
        catch(Exception ex)
        {
            Log.Fatal(ex,"Host encerrado.");
        }
        finally
        {
            Log.Information("Encerrando aplicação.");
            Log.CloseAndFlush();
        }
    }
}


