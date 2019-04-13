using Microsoft.Extensions.FileProviders;

namespace Myvas.AspNetCore.Email
{
    public class EmailTemplateOptions
    {
        /// <summary>
        /// Gets or sets the absolute path to the directory that contains the email template files.
        /// </summary>
        public string EmailTemplateRootPath { get; set; }
    }
}