using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.Email
{
    public class EmailSender : IEmailSender
    {
        protected readonly EmailOptions _options;

        public EmailSender(IOptions<EmailOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
            _options.Validate();
        }

        public virtual async Task<bool> SendEmailAsync(string recipients, string subject, string body)
        {
            return await SendEmailAsync(new EmailDto()
            {
                Recipients = recipients,
                Subject = subject,
                Body = body
            });
        }

        public virtual async Task<bool> SendEmailAsync(EmailDto input)
        {
            await Task.FromResult(0);

            string senderEmail = _options.SenderAccount;
            string senderPassword = _options.SenderPassword;
            string senderDisplayName = _options.SenderDisplayName;
            var msg = new MimeMessage();

            msg.From.Add(ParseInternetAddresses(senderEmail)[0]);
            msg.Subject = input.Subject;
            msg.Body = new TextPart(input.IsBodyHtml ? TextFormat.Html : TextFormat.Plain) { Text = input.Body };

            msg.To.AddRange(ParseInternetAddresses(input.Recipients));
            msg.Cc.AddRange(ParseInternetAddresses(input.Cc));
            msg.ReplyTo.AddRange(ParseInternetAddresses(input.ReplyTo));

            using (var client = new SmtpClient())
            {
                try
                {
                    // Accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(_options.SmtpServerAddress, _options.SmtpServerPort, _options.EnableSsl);
                    client.Authenticate(senderEmail, senderPassword);

                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
            return true;
        }

        protected static List<InternetAddress> ParseInternetAddresses(string internetAddresses)
        {
            var result = new List<InternetAddress>();

            if (!string.IsNullOrWhiteSpace(internetAddresses))
            {
                foreach (var mailAddress in internetAddresses.Split(';'))
                {
                    var s = mailAddress.Trim();
                    InternetAddress resultItem;
                    if (InternetAddress.TryParse(s, out resultItem))
                    {
                        result.Add(resultItem);
                    }
                }
            }
            return result;
        }
    }
}