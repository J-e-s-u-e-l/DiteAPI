using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class ChangeAcademyMemberRoleCommand : IRequest<BaseResponse>
    {
        public Guid MemberId { get; set; }
        public string NewRoleName { get; set; }
        public List<Guid>? AssignedTracksIds { get; set; }
        public Guid AcademyId { get; set; }
    }

    public class ChangeAcademyMemberRoleValidator : AbstractValidator<ChangeAcademyMemberRoleCommand>
    {
        public ChangeAcademyMemberRoleValidator()
        {
            RuleFor(x => x.NewRoleName)
                .NotEmpty().NotNull().WithMessage("RoleName is required");
            RuleFor(x => x.MemberId)
                .NotEmpty().NotNull().WithMessage("UserId of Member is required");
        }
    }
}
