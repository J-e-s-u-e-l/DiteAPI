using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class DeleteNotificationCommand : IRequest<BaseResponse>
    {
        public Guid NotificationId { get; set; }
    }

    public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().NotNull().WithMessage("NotificationId is required");
        }
    }
}
