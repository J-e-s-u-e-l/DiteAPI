using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAllTracksQuery : IRequest<BaseResponse<IEnumerable<GetAllTracksResponse>>>
    {
        public Guid AcademyId { get; set; }
        public GetAllTracksQuery(Guid academyId)
        {
            AcademyId = academyId;
        }
    }

    public class GetAllTracksQueryValidator : AbstractValidator<GetAllTracksQuery>
    {
        public GetAllTracksQueryValidator()
        {
            RuleFor(x => x.AcademyId).NotNull().NotEmpty().WithMessage("AcademyId is required");
        }
    }
}
