using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    /*public class ChangeAcademyMemberRoleCommand : IRequest<BaseResponse>
    {
        private Guid _academyId;
        private Guid _memberId;
        private List <Guid>? _assignedTracksIds;

        public Guid AcademyId 
        {
            get => _academyId;
            set => _academyId = value; 
        }
        public string AcademyIdFromRequest
        {
            set => _academyId = GuidHelper.ParseGuid(value, nameof(AcademyId));
        }

        public Guid MemberId { 
            get => _memberId;
            set => _memberId = value; 
        }

        public string MemberIdFromRequest
        {
            set => _memberId = GuidHelper.ParseGuid(value, nameof(_memberId));
        }

        //public string NewRole { get; set; }
        public string NewRole { get; set; }

        public List<Guid>? AssignedTracksIds {
            get => _assignedTracksIds;
            set => _assignedTracksIds = value;
        }

        public IEnumerable<string> AssignedTracksIdsFromRequest
        {
            set => _assignedTracksIds = GuidHelper.ParseGuidList(value, nameof(AssignedTracksIds));
        }
    }*/
    public class ChangeAcademyMemberRoleCommand : IRequest<BaseResponse>
    {
        public Guid AcademyId { get; set; }
        public Guid MemberId { get; set; }
        public List<Guid>? AssignedTracksIds { get; set; }
        public string NewRole { get; set; }
    }


    public class ChangeAcademyMemberRoleValidator : AbstractValidator<ChangeAcademyMemberRoleCommand>
    {
        public ChangeAcademyMemberRoleValidator()
        {
            RuleFor(x => x.NewRole)
                .NotEmpty().NotNull().WithMessage("RoleName is required");
            RuleFor(x => x.MemberId)
                .NotEmpty().NotNull().WithMessage("UserId of Member is required");
        }
    }
}
