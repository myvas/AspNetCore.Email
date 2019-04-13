using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace Myvas.AspNetCore.Email
{
    public class EmailTemplate : IEmailTemplate
    {
        private readonly EmailTemplateOptions _options;

        public EmailTemplate(IOptions<EmailTemplateOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        public string GetContent(string templateRelativePath, params string[] data)
        {
            var content = ReadPhysicalFile(templateRelativePath);

            var msg = string.Format(content, data);
            return msg;
        }

        public string GetContent(string templateRelativePath, Dictionary<string, string> data)
        {
            var content = ReadPhysicalFile(templateRelativePath);

            var msg = content;
            foreach (var item in data?.Keys)
            {
                msg = msg.Replace(item, data[item]);
            }
            return msg;
        }


        private string ReadPhysicalFile(string templateRelativePath)
        {
            var filePath = Path.Combine(_options.EmailTemplateRootPath, templateRelativePath);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Template file located at \"{templateRelativePath}\" was not found");
            }

            return File.ReadAllText(filePath);
        }
    }
}
