using DiteAPI.infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class CreateAcademyCommand : IRequest<BaseResponse>
    {
        public string AcademyName { get; set; }
        public List<string> Tracks { get; set; }
        public string Description { get; set; }
    }

    public class CreateAcademyValidator : AbstractValidator<CreateAcademyCommand>
    {
        public CreateAcademyValidator() 
        {
            RuleFor(x => x.AcademyName)
                .NotNull()
                .NotEmpty().WithMessage("Academy Name is required")
                .MaximumLength(100).WithMessage("Academy Name must not be greater than 100 characters.");

            RuleFor(x => x.Tracks)
                .Must(x => x.Any(y => !string.IsNullOrWhiteSpace(y)))
                .WithMessage("At least one track name must be provided and cannot be empty.");
        }
    }
}
