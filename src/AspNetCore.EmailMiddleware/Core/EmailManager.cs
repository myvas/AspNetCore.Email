using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspNetCore.EmailMiddleware.Services
{
    public class EmailManager
    {
        protected EmailOptions Options;

        public EmailManager(EmailOptions options)
        {
            Options = options;
        }

        public async Task SendAsync(EmailDto input)
        {
            string senderAccount = Options.SenderAccount;
            string senderPassword = Options.SenderPassword;
            string senderDisplayName = Options.SenderDisplayName;
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(senderAccount);
            msg.Subject = input.Subject;
            msg.Body = input.Body;
            msg.IsBodyHtml = input.IsBodyHtml;

            string[] recipients = input.Recipients.Split(',');
            foreach (string recipient in recipients)
            {
                msg.To.Add(new MailAddress(recipient.Trim()));
            }

            string[] ccRecipients = input.Cc.Split(',');
            foreach (string cc in ccRecipients)
            {
                msg.CC.Add(new MailAddress(cc.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(input.ReplyTo))
            {
                msg.ReplyToList.Add(new MailAddress(input.ReplyTo));
            }

            using (SmtpClient client = new SmtpClient(Options.SmtpServerAddress))
            {
                client.Port = 25;
                client.EnableSsl = false;

                client.UseDefaultCredentials = false;
                NetworkCredential cred = new NetworkCredential(senderAccount, senderPassword);
                client.Credentials = cred;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            await Task.FromResult(0);
        }
    }
}
