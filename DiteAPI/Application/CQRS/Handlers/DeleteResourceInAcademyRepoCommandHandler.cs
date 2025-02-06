using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class DeleteResourceInAcademyRepoCommandHandler : IRequestHandler<DeleteResourceInAcademyRepoCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<DeleteResourceInAcademyRepoCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteResourceInAcademyRepoCommandHandler(DataDBContext dbContext, ILogger<DeleteResourceInAcademyRepoCommandHandler> logger, IOptions<AppSettings> appSettings, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> Handle(DeleteResourceInAcademyRepoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var resourceToBeDeleted = await _dbContext.Resources
                                                                .FirstOrDefaultAsync(r => r.Id == request.ResourceId);
/*
                    if (resourceToBeDeleted == null)
                    {
                        return NotFound();
                    }*/

                    _dbContext.Resources.Remove(resourceToBeDeleted);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    _fileService.DeleteFile(resourceToBeDeleted.ResourcePath);

                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse(true, "Resource deleted successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"DELETE_RESOURCE_IN_ACADEMY_REPO_COMMAND_HANDLER  => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"DELETE_RESOURCE_IN_ACADEMY_REPO_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
