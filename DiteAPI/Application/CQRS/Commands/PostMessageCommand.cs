using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class PostMessageCommand : IRequest<BaseResponse<PostMessageResponse>>
    {
        public string MessageTitle{ get; set; }
        public string MessageBody{ get; set; }
        public Guid? TrackId { get; set; }
        public Guid? AcademyId { get; set; }
    }

    public class PostMessageCommandValidator : AbstractValidator<PostMessageCommand>
    {
        public PostMessageCommandValidator()
        {
            RuleFor(x => x.MessageTitle)
                .NotEmpty().NotNull()
                .WithMessage("MessageTitle is Required.");

            RuleFor(x => x.MessageBody)
                .NotEmpty().NotNull()
                .WithMessage("MessageBody is Required.");
        }
    }
}
