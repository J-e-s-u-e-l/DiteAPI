using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Queries
{
    public class GetTaskCompletionRateQuery : IRequest<BaseResponse<GetTaskCompletionRateResponse>>
    {
        public string TimeFilter { get; set; }
    }

    public class GetTaskCompletionRateQueryValidator : AbstractValidator<GetTaskCompletionRateQuery>
    {
        public GetTaskCompletionRateQueryValidator()
        {
            RuleFor(x => x.TimeFilter).NotNull().NotEmpty().WithMessage("TimeFilter is required");
        }
    }
}
