using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.EntityFrameworkCore;
using AspNetCore.EmailMiddleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace AspNetCore.WebApi.EmailApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

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
                           builder.AllowAnyOrigin();
                       });
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
            });

            // Add framework services.
            services.AddEmail(options =>
            {
                options.SmtpServerAddress = Configuration["SmtpServerAddress"];
                options.SenderAccount = Configuration["SenderAccount"];
                options.SenderPassword = Configuration["SenderPassword"];
                options.SenderDisplayName = Configuration["SenderDisplayName"];
            });

            // Inject an implementation of ISwaggerProvider with defaulted settings applied.
            services.AddSwaggerGen();

            // Add the detail information for the API.
            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info
                {
                    Version = "v1",
                    Title = "Email API",
                    Description = "A simple ASP.NET Core Web API for Email service",
                    TermsOfService = "None",
                    License = new Swashbuckle.Swagger.Model.License
                    {
                        Name = "Apache License 2.0",
                        Url = "https://www.apache.org/licenses/LICENSE-2.0.html"
                    }
                });

                // Determine base path for the application.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                // Set the comments path for the swagger json and ui.
                Directory.GetFiles(basePath, "*.xml", SearchOption.TopDirectoryOnly)
                    .Select(x => Path.GetFullPath(x))
                    .ToList()
                    .ForEach(x => options.IncludeXmlComments(x));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                //TODO: 因为微软的Cors 没用明白，这里补充，但不规范，这里先放开不影响开发，但后续应该去掉
                if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
                    context.Response.Headers.Add("Access-Control-Allow-Origin", new Microsoft.Extensions.Primitives.StringValues("*"));
                if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Headers"))
                    context.Response.Headers.Add("Access-Control-Allow-Headers", new Microsoft.Extensions.Primitives.StringValues(new string[] { "accept", "authorization", "content-type" }));
                if (!context.Response.Headers.ContainsKey("Access-Control-Request-Method"))
                    context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                //if (!context.Response.Headers.ContainsKey("Access-Control-Request-Method"))
                //{
                //    context.Response.Headers.Add("Access-Control-Request-Method", new Microsoft.Extensions.Primitives.StringValues("*"));
                //}
                await next();
            });
            app.UseCors("AllowAllOrigins");

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }
    }
}
