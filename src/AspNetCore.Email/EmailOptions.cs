using System;

namespace AspNetCore.Email
{
    public class EmailOptions
    {
        /// <summary>
        /// Gets or sets the name or IP Address of the host used for SMTP transactions.
        /// </summary>
        public string SmtpServerAddress { get; set; }

        /// <summary>
        /// Gets or sets the port used for SMTP transactions.
        /// </summary>
        public int SmtpServerPort { get; set; } = 25;

        /// <summary>
        /// Specify whether the <see cref="SmtpClient"/> uses Secure Sockets Layer (SSL) to encrypt the connection.
        /// </summary>
        public bool EnableSsl { get; set; } = false;

        /// <summary>
        /// Gets or sets the account used for SMTP transactions.
        /// </summary>
        public string SenderAccount { get; set; }

        /// <summary>
        /// Gets or sets the password used for SMTP transactions.
        /// </summary>
        public string SenderPassword { get; set; }

        /// <summary>
        /// Gets or sets the display name used for SMTP transactions.
        /// </summary>
        public string SenderDisplayName { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(SmtpServerAddress))
            {
                throw new ArgumentNullException(nameof(SmtpServerAddress));
            }
            if (string.IsNullOrWhiteSpace(SenderAccount))
            {
                throw new ArgumentNullException(nameof(SenderAccount));
            }
            if (SenderPassword == null)
            {
                throw new ArgumentNullException(nameof(SenderPassword));
            }
        }
    }
}
