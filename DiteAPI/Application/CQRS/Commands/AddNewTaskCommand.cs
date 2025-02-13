using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class AddNewTaskCommand : IRequest<BaseResponse<AddNewTaskResponse>>
    {
        public string TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string TaskDueDate { get; set; }
        public string? TaskCourseTag { get; set; }
    }
    public class AddNewTaskCommandValidator : AbstractValidator<AddNewTaskCommand>
    {
        public AddNewTaskCommandValidator()
        {
            RuleFor(x => x.TaskTitle)
                .NotEmpty().NotNull()
                .WithMessage("TaskTitle is Required.");

            RuleFor(x => x.TaskDueDate)
                .NotEmpty().NotNull()
                .WithMessage("TaskDueDate is Required.");
        }
    }
}
