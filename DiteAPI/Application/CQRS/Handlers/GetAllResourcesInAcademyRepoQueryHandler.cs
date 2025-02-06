using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllResourcesInAcademyRepoQueryHandler : IRequestHandler<GetAllResourcesInAcademyRepoQuery, BaseResponse<GetAllResourcesInAcademyRepoResponse>>
    {

        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllResourcesInAcademyRepoQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IFileService _fileService;

        public GetAllResourcesInAcademyRepoQueryHandler(DataDBContext dbContext, ILogger<GetAllResourcesInAcademyRepoQueryHandler> logger, IOptions<AppSettings> options, IFileService fileService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _fileService = fileService;
        }
        public async Task<BaseResponse<GetAllResourcesInAcademyRepoResponse>> Handle(GetAllResourcesInAcademyRepoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resourcesInAcademy = await _dbContext.Resources
                                                            .Where(r => r.AcademyId == request.AcademyId)
                                                            .Select(resource => new ResourceDto
                                                            {
                                                                ResourceId = resource.Id,
                                                                ResourceName = resource.ResourceName,
                                                                ResourceType = resource.ResourceType
                                                            }).ToListAsync();

                var response = new GetAllResourcesInAcademyRepoResponse
                {
                    Resources = resourcesInAcademy
                };

                return new BaseResponse<GetAllResourcesInAcademyRepoResponse>(true, "Resources in academy repository has been successfully fetched", response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_MESSAGES_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetAllResourcesInAcademyRepoResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
