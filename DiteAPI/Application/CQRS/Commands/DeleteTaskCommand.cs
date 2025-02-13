using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class DeleteTaskCommand : IRequest<BaseResponse>
    {
        public Guid TaskId { get; set; }
    }

    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(x => x.TaskId).NotNull().NotEmpty().WithMessage("TaskId is required");
        }
    }
}
