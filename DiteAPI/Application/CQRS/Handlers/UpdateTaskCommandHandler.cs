using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, BaseResponse<TasksDto>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateTaskCommandHandler(DataDBContext dbContext, ILogger<UpdateTaskCommandHandler> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<TasksDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    //var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    var task = await _dbContext.Tasks.FindAsync(request.TaskId);
                    if (task == null)
                    {
                        return new BaseResponse<TasksDto>(false, "Task not found");
                    }

                    // Update only the fields provided
                    if (!string.IsNullOrEmpty(request.TaskTitle)) task.Title = request.TaskTitle; 
                    if (!string.IsNullOrEmpty(request.TaskDescription)) task.Description = request.TaskDescription;
                    if (!string.IsNullOrEmpty(request.TaskDueDate) && DateTimeOffset.TryParse(request.TaskDueDate, out DateTimeOffset dueDate)) { task.DueDate = dueDate; } 
                    if (!string.IsNullOrEmpty(request.TaskCourseTag)) task.CourseTag = request.TaskCourseTag;
                    if (!string.IsNullOrEmpty(request.TaskStatus) && Enum.TryParse(request.TaskStatus, out TaskStatusEnum status)) { task.Status = status; }
                    
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    var response = new TasksDto
                    {
                        TaskId = task.Id,
                        TaskTitle = task.Title,
                        TaskDescription = task.Description,
                        TaskDueDate = task.DueDate.UtcDateTime,
                        TaskCourseTag = task.CourseTag,
                        TaskStatus = task.Status.ToString()
                    };

                    return new BaseResponse<TasksDto>(true, "Task updated successfully", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"UPDATE_TASK_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse<TasksDto>(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UPDATE_TASK_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<TasksDto>(false, _appSettings.ProcessingError);
            }
        }
    }
}
