using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAllMembersQuery : IRequest<BaseResponse<GetAllMembersResponse>>
    {
        public Guid AcademyId { get; set; }

        public GetAllMembersQuery(Guid academyId)
        {
            AcademyId = academyId;
        }
    }

    public class GetAllMembersQueryValidator : AbstractValidator<GetAllMembersQuery>
    {
        public GetAllMembersQueryValidator()
        {
            RuleFor(x => x.AcademyId).NotNull().NotEmpty().WithMessage("AcademyId is required");
        }
    }
}
