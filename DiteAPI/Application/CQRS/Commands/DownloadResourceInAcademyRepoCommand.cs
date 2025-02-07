using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiteAPI.Api.Application.CQRS.Commands
{
    public class DownloadResourceInAcademyRepoCommand : IRequest<BaseResponse<DownloadResourceInAcademyRepoResponse>>
    {
        public Guid ResourceId { get; set; }
    }

    public class DownloadResourceInAcademyRepoCommandValidator : AbstractValidator<DownloadResourceInAcademyRepoCommand>
    {
        public DownloadResourceInAcademyRepoCommandValidator()
        {
            RuleFor(x => x.ResourceId).NotNull().NotEmpty().WithMessage("ResourceId is required");
        }
    }
}
