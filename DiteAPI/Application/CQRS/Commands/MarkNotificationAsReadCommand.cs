using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class MarkNotificationAsReadCommand : IRequest<BaseResponse>
    {
        public Guid NotificationId { get; set; }
    }

    public class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
    {
        public MarkNotificationAsReadCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().NotNull().WithMessage("NotificationId is required");
        }
    }
}
