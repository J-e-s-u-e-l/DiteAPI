using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class SendOtpCommand : IRequest<BaseResponse>
    {
#nullable disable
        public OtpRecipientTypeEnum RecipientType { get; set; }
        public string Recipient { get; set; }
        public VerificationPurposeEnum Purpose { get; set; }
    }

    public class SendOtpRequestValidator : AbstractValidator<SendOtpCommand>
    {
        public SendOtpRequestValidator()
        {
            RuleFor(x => x.RecipientType).NotNull().NotEmpty().WithMessage("Recipient type is required");
            RuleFor(x => x.Recipient).NotNull().NotEmpty().WithMessage("Recipient value is required");
            RuleFor(x => x.Purpose).NotNull().NotEmpty().WithMessage("Purpose value is required");
        }
    }
}
