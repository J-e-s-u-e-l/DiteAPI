using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class UploadResourceCommandHandler : IRequestHandler<UploadResourceCommand, BaseResponse<UploadResourceResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<UploadResourceCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public UploadResourceCommandHandler(DataDBContext dbContext, ILogger<UploadResourceCommandHandler> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<BaseResponse<UploadResourceResponse>> Handle(UploadResourceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    var resourcePath = await _fileService.SaveFileAsync(request.File, request.AcademyId);


                    // Persist resource
                    var resource = new Resources
                    {
                        AcademyId = request.AcademyId,
                        ResourceName = request.NewResource.ResourceName,
                        ResourceType = request.NewResource.ResourceType,
                        ResourcePath = resourcePath,
                        UploadedBy = userId,
                    };

                    var newRresource = new ResourceDto
                    {
                        ResourceId = resource.Id,
                        ResourceName = resource.ResourceName,
                        ResourceType = resource.ResourceType,
                    };

                    UploadResourceResponse response = new UploadResourceResponse
                    {
                        NewResource = newRresource,
                    };

                    await _dbContext.Resources.AddAsync(resource);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse<UploadResourceResponse>(true, "Resource uploaded successfully", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"UPLOAD_RESOURCE_COMMAND_HANDLER   => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse<UploadResourceResponse>(false, _appSettings.ProcessingError);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"UPLOAD_RESOURCE_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<UploadResourceResponse>(false, _appSettings.ProcessingError);
            }
        }
    }
}
