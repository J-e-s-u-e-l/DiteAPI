using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiteAPI.infrastructure.Infrastructures.Utilities.Enums
{
internal class Enums { }
    
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female,
        [Description("Other")]
        Other
    }

    public enum OtpVerificationPurposeEnum
    {
        [Description("Email Confirmation")]
        EmailConfirmation,
            
        [Description("Password Reset")]
        PasswordReset
    }

    public enum OtpRecipientTypeEnum
    {
        [Description("Phone Number")]
        PhoneNumber,
            
        [Description("Email Address")]
        EmailAddress
    }

    public enum OtpCodeLengthEnum
    {
        [Description("Four")]
        Four = 1,
            
        [Description("Six")]
        Six
    }

    public enum EmailTitleEnum
    {
        EMAILVERIFICATION = 1,
        PASSWORDRESET,
        WELCOME
    }
}

