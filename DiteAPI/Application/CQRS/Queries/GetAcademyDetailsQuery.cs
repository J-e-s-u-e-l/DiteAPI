using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAcademyDetailsQuery : IRequest<BaseResponse<GetAcademyDetailsResponse>>
    {
        public Guid AcademyId { get; set; }

        public GetAcademyDetailsQuery(Guid academyId)
        {
            AcademyId = academyId;
        }
    }

    public class GetAcademyDetailsQueryValidator : AbstractValidator<GetAcademyDetailsQuery>
    {
        public GetAcademyDetailsQueryValidator()
        {
            RuleFor(x => x.AcademyId).NotNull().NotEmpty().WithMessage("AcademyId is required");
        }
    }
}
