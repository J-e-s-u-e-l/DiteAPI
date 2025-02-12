using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Data.Models
{
    public class ValidationResultModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Expires { get; set; }
    }

    public class CreateAcademyResponse
    {
        public Guid AcademyId { get; set; }
        public string AcademyName { get; set; }
        public List<string> TrackNames { get; set; }
        public string AcademyDescription { get; set; }
    }

    public class JoinAcademyResponse
    {
        public Guid AcademyId { get; set; }
    }

    public class GetUserAcademiesResponse
    {
        public Guid AcademyId { get; set; }
        public string AcademyName { get; set; }
        public string AcademyDescription { get; set; }
        public string AcademyMembersCount { get; set; }
        public string AcademyTracksCount { get; set; }
        public bool AcademyCreatedByUser { get; set; }
    }

    public class GetAcademyDetailsResponse
    {
        public string AcademyName { get; set; }
    }

    public class GetAllMembersResponse
    {
        public bool IsAnAdminInTheAcademy { get; set; }
        public List<AcademyMemberDetails> Members { get; set; }
    }
    
    public class GetAllTracksResponse
    {
        public Guid TrackId { get; set; }
        public string TrackName { get; set; }
    }

    public class PostMessageResponse
    {
        public Guid MessageId { get; set; }
    }
    public class GetAllMessagesResponse
    {
        public int RemainingMessagesCount { get; set; }
        public List<MessageDto> Messages { get; set; }
    }

    public class GetAllNotificationsResponse
    {
        public Guid NotificationId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationBody{ get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }

    public class GetUnreadNotificationsCountResponse
    {
        public int UnreadNotificationsCount { get; set; }
    }

    public class GetAcademyInfoResponse
    {
        public string AcademyCode { get; set; }
    }

    public class GetMessageDetailsResponse
    {
        public List<MessageDto> Message { get; set; }
        public List<ResponseDto> Responses { get; set; }
    }

    public class UploadResourceResponse
    {
        public ResourceDto NewResource { get; set; }
    }

    public class GetAllResourcesInAcademyRepoResponse
    {
        public List<ResourceDto> Resources { get; set; }
    }

    public class DownloadResourceInAcademyRepoResponse
    {
        public Stream Resource { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }

    public class AddNewTaskResponse
    {
        public Guid TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset TaskDueDate { get; set; }
        public string TaskCourseTag { get; set; }
        public string TaskStatus { get; set; }
    }

    public class GetTaskStatusEnumValuesResponse
    {
        public List<String> TaskStatuses { get; set; }
    }

    public class GetAllTasksResponse
    {
        public List<TasksDto> Tasks { get; set; }
    }

    public class JwtRequest
    {
    #nullable disable
        public string Username { get; set; }
        public string EmailAddress{ get; set; }
        public Guid UserId { get; set; }
    }
}
