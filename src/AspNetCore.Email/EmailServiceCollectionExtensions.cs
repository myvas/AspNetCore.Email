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
        /// <param name="services">The <see cref="IServiceCollection"/> passed to the configuration method.</param>
        /// <param name="setupAction">The middleware configuration options.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.TryAddSingleton<IEmailSender, EmailService>();

            return services;
        }

        /// <summary>
        /// Using Email Middleware
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> passed to the configuration method.</param>
        /// <param name="setupAction">The middleware configuration options.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            return services.AddEmail(setupAction: null);
        }

    }
}
