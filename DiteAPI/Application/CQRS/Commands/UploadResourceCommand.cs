using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Data.Entities;
using FluentValidation;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class UploadResourceCommand : IRequest<BaseResponse<UploadResourceResponse>>
    {
        public IFormFile File { get; set; }
        public Guid AcademyId { get; set; }
        public Resources NewResource { get; set; }
    }

    public class UploadResourceCommandValidator : AbstractValidator<UploadResourceCommand>
    {
        public UploadResourceCommandValidator()
        {
            RuleFor(x => x.AcademyId)
                .NotEmpty().NotNull()
                .WithMessage("AcademyId is Required.");

            RuleFor(x => x.NewResource)
                .NotEmpty().NotNull()
                .WithMessage("NewResource is Required.");

            RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(file => file!= null && file.Length > 0).WithMessage("File cannot be empty.")
            .Must(file => file.Length <= 5 * 1024 * 1024) // 5 MB limit
                .WithMessage("File size must be less than 5 MB.")
            .Must(file => IsValidFileType(file)).WithMessage("Invalid file type. Allowed types: .jpg, .png, .pdf.");   
    }
        private bool IsValidFileType(IFormFile file)
        {
            if (file == null) return false;

            var allowedExtensions = new[] { ".jpg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}
