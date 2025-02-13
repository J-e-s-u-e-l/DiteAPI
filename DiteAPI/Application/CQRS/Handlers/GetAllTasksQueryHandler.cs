using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, BaseResponse<GetAllTasksResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllTasksQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllTasksQueryHandler(DataDBContext dbContext, ILogger<GetAllTasksQueryHandler> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<GetAllTasksResponse>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                var tasks = await _dbContext.Tasks
                                    .Where(t => t.UserId == userId)
                                    .Select(t => new TasksDto
                                    {
                                        TaskId = t.Id,
                                        TaskTitle = t.Title,
                                        TaskDescription = t.Description,
                                        TaskDueDate = t.DueDate.UtcDateTime,
                                        TaskCourseTag = t.CourseTag,
                                        TaskStatus = t.Status.ToString()
                                    })
                                    .ToListAsync();

                var response = new GetAllTasksResponse
                {
                    Tasks = tasks
                };

                return new BaseResponse<GetAllTasksResponse>(true, "Tasks fetched successfully", response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_TASKS_QUERY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetAllTasksResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
