namespace AspNetCore.Email
{
    /// <summary>
    /// The DTO used for WebApi
    /// </summary>
    public class EmailDto
    {
        /// <summary>
        /// Gets or sets the recipients of email.
        /// </summary>
        public string Recipients { get; set; }
        /// <summary>
        /// Gets or sets the subject line for this e-mail message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Specify whether this e-mail message <see cref="Body"/> is in Html.
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Gets or sets the e-mail message body.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Gets or sets the address colleciton that contains the carbon copy (CC) recipients for this e-mail message.
        /// </summary>
        public string Cc { get; set; }
        /// <summary>
        /// Gets or sets the e-mail address for recipients to reply to.
        /// </summary>
        public string ReplyTo { get; set; }
    }
}
