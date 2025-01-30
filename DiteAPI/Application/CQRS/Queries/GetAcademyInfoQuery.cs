using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAcademyInfoQuery : IRequest<BaseResponse<GetAcademyInfoResponse>>
    {
        public Guid AcademyId { get; set; }
    }

    public class GetAcademyInfoQueryValidator : AbstractValidator<GetAcademyInfoQuery>
    {
        public GetAcademyInfoQueryValidator()
        {
            RuleFor(x => x.AcademyId)
                .NotEmpty().NotNull().WithMessage("AcademyId is required");
        }
    }
}
