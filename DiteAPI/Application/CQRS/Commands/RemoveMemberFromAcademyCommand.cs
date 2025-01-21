using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class RemoveMemberFromAcademyCommand : IRequest<BaseResponse>
    {
        public Guid MemberId { get; set; }
        public Guid AcademyId { get; set; }
    }

    public class RemoveMemberFromAcademyCommandValidator : AbstractValidator<RemoveMemberFromAcademyCommand>
    {
        public RemoveMemberFromAcademyCommandValidator()
        {
            RuleFor(x => x.AcademyId).NotNull().NotEmpty().WithMessage("AcademyId is required");
            RuleFor(x => x.MemberId).NotNull().NotEmpty().WithMessage("MemberId is required");
        }
    }
}
