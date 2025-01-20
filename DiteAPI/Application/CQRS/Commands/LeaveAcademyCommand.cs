using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    //public class LeaveAcademyCommand : IRequest<BaseResponse<LeaveAcademyResponse>>
    public class LeaveAcademyCommand : IRequest<BaseResponse>
    {
        public Guid AcademyId { get; set; }

        public LeaveAcademyCommand(Guid academyId)
        {
            AcademyId = academyId;
        }
    }

    public class LeaveAcademyCommandValidator : AbstractValidator<LeaveAcademyCommand>
    {
        public LeaveAcademyCommandValidator()
        {
            RuleFor(x => x.AcademyId).NotNull().NotEmpty().WithMessage("AcademyId is required");
        }
    }
}
