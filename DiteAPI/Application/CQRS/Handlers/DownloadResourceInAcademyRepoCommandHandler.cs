using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class DownloadResourceInAcademyRepoCommandHandler : IRequestHandler<DownloadResourceInAcademyRepoCommand, BaseResponse<DownloadResourceInAcademyRepoResponse>>
    {

        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllTracksQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IFileService _fileService;

        public DownloadResourceInAcademyRepoCommandHandler(DataDBContext dbContext, ILogger<GetAllTracksQueryHandler> logger, IOptions<AppSettings> appSettings, IFileService fileService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _fileService = fileService;
        }

        public async Task<BaseResponse<DownloadResourceInAcademyRepoResponse>> Handle(DownloadResourceInAcademyRepoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resourceToBeRetrieved = await _dbContext.Resources.FirstOrDefaultAsync(x => x.Id == request.ResourceId);

                var fileStream = await _fileService.GetFileAsync(resourceToBeRetrieved.ResourcePath);

                DownloadResourceInAcademyRepoResponse response = new DownloadResourceInAcademyRepoResponse
                {
                    Resource = fileStream
                };

                return new BaseResponse<DownloadResourceInAcademyRepoResponse>(true, "Resource downloding...", response);
            }

            catch (Exception ex)
            {
                _logger.LogError($"DOWNLOAD_RESOURCE_IN_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<DownloadResourceInAcademyRepoResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }

        Task<BaseResponse<DownloadResourceInAcademyRepoResponse>> IRequestHandler<DownloadResourceInAcademyRepoCommand, BaseResponse<DownloadResourceInAcademyRepoResponse>>.Handle(DownloadResourceInAcademyRepoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
