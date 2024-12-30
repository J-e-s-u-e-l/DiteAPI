using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class UniqueUsernameCheckRequest : IRequest<BaseResponse>
    {
        public string? Username { get; set; }
    }

    public class UniqueUsernameCheckValidator : AbstractValidator<UniqueEmailCheckRequest>
    {
        public UniqueUsernameCheckValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Username is required");
        }
    }
}
