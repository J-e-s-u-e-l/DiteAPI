using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class UniqueEmailCheckRequest : IRequest<BaseResponse>
    {
        public string? Email { get; set; }
    }

    public class UniqueEmailCheckValidator : AbstractValidator<UniqueEmailCheckRequest>
    {
        public UniqueEmailCheckValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required").EmailAddress();
        }
    }
}
