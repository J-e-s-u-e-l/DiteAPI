using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DiteAPI.infrastructure.Infrastructures.Utilities.Enums
{

    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female,
        [Description("Other")]
        Other
    }

    public enum VerificationPurposeEnum
    {
        [Description("Email Confirmation")]
        EmailConfirmation = 1,
            
        [Description("Password Reset")]
        PasswordReset
    }

    public enum OtpRecipientTypeEnum
    {    
        [Description("Email")]
        Email = 1,
        [Description("Phone Number")]
        PhoneNumber
        
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

    public enum OtpTokenStatusEnum
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

    public enum TaskStatusEnum
    {
        [Description("Pending")]
        Pending = 1,
        [Description("InProgress")]
        InProgress,
        [Description("Completed")]
        Completed
    }

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
                        return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            return GenericEnum.ToString();
        }
    }

    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}


