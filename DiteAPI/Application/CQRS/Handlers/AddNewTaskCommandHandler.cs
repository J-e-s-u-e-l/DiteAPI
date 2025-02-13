using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Data.Entities;
using MediatR;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class AddNewTaskCommandHandler : IRequestHandler<AddNewTaskCommand, BaseResponse<AddNewTaskResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<AddNewTaskCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddNewTaskCommandHandler(DataDBContext dbContext, ILogger<AddNewTaskCommandHandler> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<AddNewTaskResponse>> Handle(AddNewTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    var newTask = new Tasks
                    {
                        Title = request.TaskTitle,
                        Description = request.TaskDescription,
                        DueDate = DateTimeOffset.Parse(request.TaskDueDate),
                        CourseTag = request.TaskCourseTag,
                        Status = TaskStatusEnum.Pending,
                        UserId = userId,
                    };

                    await _dbContext.AddAsync(newTask);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    var response = new AddNewTaskResponse
                    {
                        TaskId = newTask.Id,
                        TaskTitle = request.TaskTitle,
                        TaskDescription = request.TaskDescription,
                        TaskDueDate = newTask.DueDate,
                        TaskCourseTag = request.TaskCourseTag,
                        TaskStatus = newTask.Status.ToString()
                    };

                    return new BaseResponse<AddNewTaskResponse>(true, "New Task Added", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ADD_NEW_TASK_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse<AddNewTaskResponse>(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ADD_NEW_TASK_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<AddNewTaskResponse>(false, _appSettings.ProcessingError);
            }
        }
    }
}
