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
    builder.Services.AddEmailTemplate(options =>
    {
        options.EmailTemplateRootPath = Path.Combine(builder.Environment.WebRootPath, "EmailTemplates");
    });

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();
    }

    app.UseCors("AllowAllOrigins");

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
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
