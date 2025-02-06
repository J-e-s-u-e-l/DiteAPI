using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAllResourcesInAcademyRepoQuery : IRequest<BaseResponse<GetAllResourcesInAcademyRepoResponse>>
    {
        public Guid AcademyId { get; set; }
    }

    public class GetAllResourcesInAcademyRepoQueryValidator : AbstractValidator<GetAllResourcesInAcademyRepoQuery>
    {
        public GetAllResourcesInAcademyRepoQueryValidator()
        {
            RuleFor(x => x.AcademyId)
                .NotEmpty().NotNull()
                .WithMessage("AcademyId is Required.");
        }
    }
}
