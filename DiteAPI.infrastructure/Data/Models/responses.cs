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
    #nullable disable
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
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

    public class JwtRequest
    {
    #nullable disable
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress{ get; set; }
        public Guid UserId { get; set; }
    }
}
