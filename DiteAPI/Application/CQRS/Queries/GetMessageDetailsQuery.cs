using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetMessageDetailsQuery : IRequest<BaseResponse<GetMessageDetailsResponse>>
    {
        public Guid MessageId { get; set; }
    }

    public class GetMessageDetailsQueryValidator : AbstractValidator<GetMessageDetailsQuery>
    {
        public GetMessageDetailsQueryValidator()
        {
            RuleFor(x => x.MessageId).NotNull().NotEmpty().WithMessage("AcademyId is required");
        }
    }
}
