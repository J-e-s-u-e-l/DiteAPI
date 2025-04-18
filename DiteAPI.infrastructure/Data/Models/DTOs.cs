﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Guid? AcademyId { get; set; }
        public Guid MessageId { get; set; }
        public string MessageTitle { get; set; }
        public string MessageBody { get; set; }
        public string SenderUserName { get; set; }
        public string SenderRoleInAcademy { get; set; }
        public string TrackName { get; set; }
        public string SentAtAgo { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public int TotalNumberOfResponses { get; set; }
    }

    public class NotificationDto
    {
        public Guid NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationBody { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }

    public class ResponseToMessage
    {
        public string ResponseBody { get; set; }
        public string ResponderUserName { get; set; }
        public string ResponderRoleInAcademy { get; set; }
        public DateTime SentAt { get; set; }
    }

    public class AcademyMemberDetails
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public List<Guid> assignedTracks { get; set; }
    }

    public class ResponseDto
    {
        public Guid ResponseId { get; set; }
        public string ResponseBody { get; set; }
        public string ResponderUsername { get; set; }
        public string ResponderRoleInAcademy { get; set; }
        public string SentAtAgo { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }

    public class ResourceDto
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
    }

    public class TasksDto
    {
        public Guid TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDueDate { get; set; }
        public string TaskCourseTag { get; set; }
        public string TaskStatus{ get; set; }
    }
}