using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetAllNotificationsQuery : IRequest<BaseResponse<List<GetAllNotificationsResponse>>>
    {
        // Adequate pagination will be implemented...

       /* public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;*/
    }

    /*public class GetAllNotificationsQueryValidator : AbstractValidator<GetAllNotificationsQuery>
    {
        public GetAllNotificationsQueryValidator()
        {
            RuleFor(x => x.PageNumber).NotNull().NotEmpty().WithMessage("PageNumber is required");
            RuleFor(x => x.PageSize).NotNull().NotEmpty().WithMessage("PageSize is required");
        }
    }*/
}
