using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiteAPI.infrastructure.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset TimeCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset TimeUpdated { get; set; } = DateTimeOffset.UtcNow;
    }

    public class BaseResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }

        public BaseResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }

    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
        public BaseResponse()
        {
            
        }
        public BaseResponse(bool status, string message)
        {
            Status = status;
            Message = message;
        }
        public BaseResponse(bool status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }

    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
    }

    public class ContactInformation
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class MailKitSection
    {
        public string EmailHost { get; set; }
        public string Port { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }

    }
    public class Verification
    {
        public string VerificationLinkTemplate { get; set; }
    }
    public class OtpRequestResult
    {
        public Guid UserId { get; set; }
        public string? Recipent { get; set; }   // Phone number or Email
    }

    public class VerificationLinkRequestResult
    {
        public Guid UserId { get; set; }
        public string? Recipent { get; set; }   // Phone number or Email
    }

    public class SendOTPToUser
    {
        public Guid UserId { get; set;}
        public string FirstName { get; set; } = default!;
        public string Recipient { get; set; } = default!;
        public OtpCodeLengthEnum OtpCodeLength { get; set; }
        public OtpRecipientTypeEnum RecipientType { get; set; }
        public VerificationPurposeEnum Purpose { get; set; }
    }

    public class SendVerificationLinkToUser
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string Recipient { get; set; } = default!;
        public VerificationPurposeEnum VerificationPurpose { get; set; }
    }

    public class ValidateCodeRequest
    {
        public Guid UserId { get; set; }
        public string? Code { get; set;  }
        public VerificationPurposeEnum Purpose { get; set; }
    }

    public class SendWelcomeRequest
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
    }

    public class EmailBodyResponse
    {
        public string? PlainBody { get; set; }
        public string? HtmlBody { get; set; }
    }

    public class EmailBodyRequest
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public EmailTitleEnum EmailTitle{ get; set; }
    }
}
