using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAllMessagesQuery : IRequest<BaseResponse<GetAllMessagesResponse>>
    {
        public Guid AcademyId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllMessagesQueryValidator : AbstractValidator<GetAllMessagesQuery>
    {
        public GetAllMessagesQueryValidator()
        {
            RuleFor(x => x.AcademyId).NotNull().NotEmpty().WithMessage("AcademyId is required");
        }
    }
}
