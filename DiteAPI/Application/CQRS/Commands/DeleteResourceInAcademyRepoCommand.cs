using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class DeleteResourceInAcademyRepoCommand : IRequest<BaseResponse>
    {
        public Guid ResourceId { get; set; }
    }
    public class DeleteResourceInAcademyRepoCommandValidator : AbstractValidator<DeleteResourceInAcademyRepoCommand>
    {
        public DeleteResourceInAcademyRepoCommandValidator()
        {
            RuleFor(x => x.ResourceId)
                .NotEmpty().NotNull()
                .WithMessage("ResourceId is Required.");
        }
    }
}
