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
    }

    public class MultipleEmailRequest
    {
        public string? RecipientName { get; set; }
        public IEnumerable<string>? RecipientEmailAddress { get; set; }
        public string? EmailSubject { get; set; }
        public string? HtmlEmailBody { get; set; }
        public string? PlainEmailBody { get; set; }
    }

    public class MeetchopraValidResponse
    {
        public bool status { get; set; }
    }

    public class SendGridSingleEmailResponseError
    {
        public string? Message { get; set; }
        public string? Field { get; set; }
        public string? Help { get; set; }
    }

    public class SendGridErrorResponse
    {
        public IEnumerable<SendGridSingleEmailResponseError>? Errors { get; set; }
    }

    public class ErrorResponse
    {
        public bool Status { get; set; } = false;
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public string? TraceId { get; set; }
    }

    public class MessageDto
    {
        public string MessageBody { get; set; }
        public string MessageTitle { get; set; }
        public string SenderUserName { get; set; }
        public string SenderRoleInAcademy { get; set; }
        public string Track { get; set; }
        public DateTime SentAt { get; set; }
        public List<ResponseToMessage> ResponsesToMessage { get; set; }
    }

    public class ResponseToMessage
    {
        public string ResponseBody { get; set; }
        public string ResponderUserName { get; set; }
        public string ResponderRoleInAcademy { get; set; }
        public DateTime SentAt { get; set; }
    }
}