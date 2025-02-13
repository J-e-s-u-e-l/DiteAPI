using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<DeleteTaskCommandHandler> _logger;
        private readonly AppSettings _appSettings;

        public DeleteTaskCommandHandler(DataDBContext dbContext, ILogger<DeleteTaskCommandHandler> logger, IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<BaseResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var affectedRows = await _dbContext.Tasks.Where(t => t.Id == request.TaskId)
                                                                .ExecuteDeleteAsync(cancellationToken);

                    if (affectedRows == 0)
                    {
                        return new BaseResponse(false, "Task not found");
                    }

                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();

                    return new BaseResponse(true, "Task deleted");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"DELETE_TASK_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"DELETE_TASK_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
