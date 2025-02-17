using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

// Console output in local language
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var configuration = GetConfiguration(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var assembly = typeof(Program).Assembly;
var assemblyName = assembly.GetName().Name;
var assemblyVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
Log.Information($"{assemblyName} {assemblyVersion} starting up中文...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    string connectionString = configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddLogging();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
    });
    builder.Services.AddEmail(options =>
    {
        options.SmtpServerAddress = configuration["Email:SmtpServerAddress"];
        options.SenderAccount = configuration["Email:SenderAccount"];
        options.SenderPassword = configuration["Email:SenderPassword"];
        options.SenderDisplayName = configuration["Email:SenderDisplayName"];
    });
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    // Add the detail information for the API.
    builder.Services.AddSwaggerGen(options =>
    {
        options.IgnoreObsoleteActions();
        options.IgnoreObsoleteProperties();
        options.SwaggerDoc("8.0", new OpenApiInfo
        {
            Version = "8.0",
            Title = "Email API",
            Description = "A simple ASP.NET Core Web API for Email service"
        });
    });

    var app = builder.Build();
    app.UseCors("AllowAllOrigins");
    app.UseStaticFiles();
    if (app.Environment.IsDevelopment())
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint
        app.UseSwagger();
        // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/8.0/swagger.json", "8.0");
        });
    }
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program
{
    private static IConfiguration GetConfiguration(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isDevelopment = environment == Environments.Development;

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

        if (isDevelopment)
        {
            configurationBuilder.AddUserSecrets<Program>();
        }

        var configuration = configurationBuilder.Build();

        configurationBuilder.AddCommandLine(args);
        configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }
}
