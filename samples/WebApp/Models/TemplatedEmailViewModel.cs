using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TemplatedEmailViewModel
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public string RecipientName { get; set; }
        public bool IsHtml { get; set; }
    }
}
