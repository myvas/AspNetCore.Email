using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Myvas.AspNetCore.Email;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace Myvas.AspNetCore.WebApi.EmailApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddLogging();

            // Add framework services.
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
            //});

            // Add framework services.
            services.AddEmail(options =>
            {
                options.SmtpServerAddress = Configuration["Email:SmtpServerAddress"];
                options.SenderAccount = Configuration["Email:SenderAccount"];
                options.SenderPassword = Configuration["Email:SenderPassword"];
                options.SenderDisplayName = Configuration["Email:SenderDisplayName"];
            });

            // Add the detail information for the API.
            services.AddSwaggerGen(options =>
            {
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Email API V1",
                    Description = "A simple ASP.NET Core Web API for Email service"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowAllOrigins");

            app.UseStaticFiles();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Host = httpReq.Host.Value);
            });

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });
        }
    }
}
