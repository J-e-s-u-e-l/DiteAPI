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

    public class JwtRequest
    {
    #nullable disable
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress{ get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateAcademyResponse
    {

    }
}
