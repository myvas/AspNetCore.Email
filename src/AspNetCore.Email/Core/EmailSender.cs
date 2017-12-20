using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Email
{
    public class EmailSender : IEmailSender
    {
        protected readonly EmailOptions _options;

        public EmailSender(IOptions<EmailOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

            if (string.IsNullOrWhiteSpace(_options.SmtpServerAddress))
            {
                throw new ArgumentException(nameof(_options.SmtpServerAddress));
            }
            if (string.IsNullOrWhiteSpace(_options.SenderAccount))
            {
                throw new ArgumentException(nameof(_options.SenderAccount));
            }
        }

        public virtual async Task SendEmailAsync(string recipients, string subject, string body)
        {
            await SendEmailAsync(new EmailDto()
            {
                Recipients = recipients,
                Subject = subject,
                Body = body
            });
        }

        public virtual async Task SendEmailAsync(EmailDto input)
        {
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
                client.Connect(_options.SmtpServerAddress, _options.SmtpServerPort, _options.EnableSsl);
                client.Authenticate(senderEmail, senderPassword);

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //client.Disconnect(true);
                }
            }
            await Task.FromResult(0);
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