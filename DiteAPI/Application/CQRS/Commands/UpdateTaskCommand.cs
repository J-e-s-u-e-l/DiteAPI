using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class UpdateTaskCommand : IRequest<BaseResponse<TasksDto>>
    {
        public Guid TaskId { get; set; }
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskDueDate { get; set; }
        public string? TaskCourseTag { get; set; }
        public string? TaskStatus { get; set; }
    }
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().NotNull()
                .WithMessage("TaskId is Required.");
        }
    }
}
