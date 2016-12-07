using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.EmailMiddleware.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.EmailMiddleware
{
    public static class EmailServiceCollectionExtensions
    {
        /// <summary>
        /// Using Email Middleware
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> passed to the configuration method</param>
        /// <param name="setupAction">Middleware configuration options</param>
        /// <returns>The updated <see cref="IApplicationBuilder"/></returns>
        public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.Configure(setupAction);
            services.TryAddSingleton<IEmailSender, EmailService>();

            return services;
        }
    }
}
