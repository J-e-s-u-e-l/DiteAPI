using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Config
{
    public class AppSettings
    {
        public string ProcessingError { get; set; }
        public string UserWithEmailNotFound { get; set; }
        public string AcademyNotFound { get; set; }
        public string AccountLocked { get; set; }
        public string EmailNotVerified { get; set; }
        public string SingInSuccessful { get; set; }
        public string NoActiveSession { get; set; }
        public string InvalidOtp { get; set; }
        public string EmailVerified { get; set; }
        public string OtpVerified { get; set; }
        public string OtpSent { get; set; }
        public string PasswordResetSuccessful { get; set; }
        public string RegistrationSuccessfully { get; set; }
        public string OtpNotVerified { get; set; }
        public string AcademyCreatedSuccessfully { get; set; }
        public string AdminRoleId { get; set; }
        public string FacilitatorRoleId { get; set; }
        public string MemberRoleId { get; set; }
        public string AcademyEnrollmentSuccessful { get; set; }
        public string Unauthorized { get; set; }
        public string UserAlreadyExistInAcademy { get; set; }
    }
}
