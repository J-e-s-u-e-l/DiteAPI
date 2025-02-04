﻿using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class PostReplyCommand : IRequest<BaseResponse>
    {
        public Guid ParentId { get; set; }
        public string ReplyBody{ get; set; }
    }

    public class PostReplyToMessageCommandValidator : AbstractValidator<PostReplyCommand>
    {
        public PostReplyToMessageCommandValidator()
        {
            RuleFor(x => x.ParentId)
                .NotEmpty().NotNull()
                .WithMessage("ParentId is Required.");

            RuleFor(x => x.ReplyBody)
                .NotEmpty().NotNull()
                .WithMessage("ReplyBody is Required.");
        }
    }
}
