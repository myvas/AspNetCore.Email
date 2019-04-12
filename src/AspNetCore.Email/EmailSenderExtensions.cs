using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Myvas.AspNetCore.Email
{
    public static class EmailSenderExtensions
    {
        /// <summary>
        /// Using Email Middleware
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> passed to the configuration method.</param>
        /// <param name="setupAction">The middleware configuration options.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailOptions> setupAction = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction != null)
            {
                services.Configure(setupAction); //IOptions<EmailOptions>
            }

            services.TryAddTransient<IEmailSender, EmailSender>();
            services.TryAddTransient<EmailSender>();

            return services;
        }
    }
}
