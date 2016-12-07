using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.EmailMiddleware.Services
{
    public class EmailManager
    {
        protected EmailOptions Options;

        public EmailManager(EmailOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.SmtpServerAddress))
            {
                throw new ArgumentException(nameof(options.SmtpServerAddress));
            }
            if (string.IsNullOrWhiteSpace(options.SenderAccount))
            {
                throw new ArgumentException(nameof(options.SenderAccount));
            }

            Options = options;
        }

        public async Task SendAsync(EmailDto input)
        {
            string senderEmail = Options.SenderAccount;
            string senderPassword = Options.SenderPassword;
            string senderDisplayName = Options.SenderDisplayName;
            var msg = new MimeMessage();
            
            msg.From.Add(ParseInternetAddresses(senderEmail)[0]);
            msg.Subject = input.Subject;
            msg.Body = new TextPart(input.IsBodyHtml ? TextFormat.Html : TextFormat.Plain) { Text = input.Body };

            msg.To.AddRange(ParseInternetAddresses(input.Recipients));
            msg.Cc.AddRange(ParseInternetAddresses(input.Cc));
            msg.ReplyTo.AddRange(ParseInternetAddresses(input.ReplyTo));

            using (var client = new SmtpClient())
            {
                client.Connect(Options.SmtpServerAddress, Options.SmtpServerPort, Options.EnableSsl);
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

        private static List<InternetAddress> ParseInternetAddresses(string internetAddresses)
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