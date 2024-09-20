using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class JoinAcademyCommand : IRequest<BaseResponse>
    {
        public string AcademyCode { get; set; }
    }

    public class JoinAcademyValidator : AbstractValidator<JoinAcademyCommand>
    {
        public JoinAcademyValidator()
        {
            RuleFor(x => x.AcademyCode)
                .NotNull()
                .NotEmpty().WithMessage("Academy Code is required")
                .MaximumLength(6).WithMessage("Academy Code cannot be greater than 6 characters.");
        }
    }
}
