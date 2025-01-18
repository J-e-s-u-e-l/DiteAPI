using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class JoinAcademyCommand : IRequest<BaseResponse<JoinAcademyResponse>>
    {
        public string AcademyCode { get; set; }
    }

    public class JoinAcademyValidator : AbstractValidator<JoinAcademyCommand>
    {
        public JoinAcademyValidator()
        {
            RuleFor(x => x.AcademyCode)
                .NotEmpty()
                .MaximumLength(8).WithMessage("Academy Code cannot be greater than 6 characters.");
        }
    }
}
