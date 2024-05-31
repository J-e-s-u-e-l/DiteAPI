using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class VerifyOtpCommand : IRequest<BaseResponse>
    {
        public string Code { get; set; } = default!;
        public VerificationPurposeEnum Purpose{ get; set; }
    }

    public class VerifyEmailRequestValidator : AbstractValidator<VerifyOtpCommand>
    {
        public VerifyEmailRequestValidator()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("Verification code is required");
            RuleFor(x => x.Purpose).NotNull().NotEmpty().IsInEnum();
        }
    }
}
