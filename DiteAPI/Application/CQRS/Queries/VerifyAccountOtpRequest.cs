using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class VerifyAccountOtpRequest : IRequest<BaseResponse>
    {
        public string Signupsessionkey { get; set; } = default!;
        public string Code { get; set; } = default!;
    }

    public class VerifyAccountOtpRequestValidator : AbstractValidator<VerifyAccountOtpRequest>
    {
        public VerifyAccountOtpRequestValidator()
        {
            RuleFor(x => x.Signupsessionkey).NotNull().NotEmpty().WithMessage("Signup session key is required");
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("OTP Code is required");
        }
    }
}
