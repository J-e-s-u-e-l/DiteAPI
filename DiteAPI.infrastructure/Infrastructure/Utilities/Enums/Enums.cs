using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DiteAPI.infrastructure.Infrastructures.Utilities.Enums
{
public static class Enums 
{
    public static string GetDescription(this Enum GenericEnum)
    {
        Type genericEnumType = GenericEnum.GetType();
        MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());

        if ((memberInfo != null && memberInfo.Length > 0))
        {
            var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if ((_Attribs != null && _Attribs.Length > 0))
            {
                return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description
            }
        }

        return GenericEnum.ToString();
    }
}

    

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
            
        [Description("Password Reset Request")]
        PasswordResetRequest
    }

    public enum OtpRecipientTypeEnum
    {
        [Description("Phone Number")]
        PhoneNumber,
            
        [Description("Email")]
        Email
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

    public enum OtpCodeStatusEnum
    {
        [Description("Sent")]
        Sent = 1,
        [Description("Verified")]
        Verified,
        [Description("Expired")]
        Expired,
        [Description("Invalidated")]
        Invalidated

    }
}

