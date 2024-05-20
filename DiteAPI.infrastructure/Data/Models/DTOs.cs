using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Models
{
    internal class DTOs { }
    public class SingleEmailRequest
    {
        public string? RecipientName { get; set; }
        public string? RecipientEmailAddress { get; set; }
        public string? EmailSubject { get; set; }
        public string? HtmlEmailBody { get; set; }
        public string? PlainEmailBody { get; set; }
        public List<string>? AttachmentBase64 { get; set; }
        public List<string>? AttachmentName { get; set; }
        public List<string>? AttachmentType { get; set; }
    }

    public class MultipleEmailRequest
    {
        public string? RecipientName { get; set; }
        public IEnumerable<string>? RecipientEmailAddress { get; set; }
        public string? EmailSubject { get; set; }
        public string? HtmlEmailBody { get; set; }
        public string? PlainEmailBody { get; set; }
    }
}
