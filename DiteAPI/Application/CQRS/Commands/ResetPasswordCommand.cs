using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class ResetPasswordCommand : IRequest<BaseResponse>
    {
#nullable disable
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }

    public class ForgotPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty().WithMessage("New password is required.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{}|;':"",<.>\/?])[A-Za-z\d!@#$%^&*()_+\-=\[\]{}|;':"",<.>\/?]{8,}$").WithMessage("Invalid password format. Your password must be at least 8 characters long and include at least one digit, one lowercase letter, one uppercase letter, and one special character.");

            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("The confirmation password does not match the entered password.");
        }
    }
}
