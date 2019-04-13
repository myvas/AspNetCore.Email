using Microsoft.Extensions.DependencyInjection;
using Myvas.AspNetCore.Email;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailTemplateServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailTemplate(this IServiceCollection services, Action<EmailTemplateOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction != null)
            {
                services.Configure(setupAction); //IOptions<EmailTemplateOptions>
            }

            services.AddTransient<IEmailTemplate, EmailTemplate>();

            return services;
        }
    }
}
