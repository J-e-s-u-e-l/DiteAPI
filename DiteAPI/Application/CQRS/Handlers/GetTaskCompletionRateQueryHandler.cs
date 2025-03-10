using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetTaskCompletionRateQueryHandler : IRequestHandler<GetTaskCompletionRateQuery, BaseResponse<GetTaskCompletionRateResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetTaskCompletionRateQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTaskCompletionRateQueryHandler(DataDBContext dbContext, ILogger<GetTaskCompletionRateQueryHandler> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<GetTaskCompletionRateResponse>> Handle(GetTaskCompletionRateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                DateTime startDate;
                DateTime endDate = DateTime.UtcNow;

                switch (request.TimeFilter.ToLower())
                {
                    case "yesterday":
                        startDate = DateTime.UtcNow.AddDays(-1);
                    break;

                    case "last_week":
                        startDate = DateTime.UtcNow.AddDays(-7);
                    break;

                    case "last_month":
                        startDate = DateTime.UtcNow.AddMonths(-1);
                    break;

                    default:
                        return new BaseResponse<GetTaskCompletionRateResponse>(false, "Invalid time filter");
                }

                var tasks = await _dbContext.Tasks
                                                .Where(t => t.UserId == userId && t.TimeCreated >= startDate && t.TimeCreated < endDate)
                                                .ToListAsync();

                int totalTasks = tasks.Count;
                int completedTasks = tasks.Count(t => t.Status == TaskStatusEnum.Completed);

                var response = new GetTaskCompletionRateResponse();

                if (totalTasks <= 0)
                {
                    response.CompletionRate = 0;
                }
                else 
                { 
                    response.CompletionRate = ((float)completedTasks / totalTasks) * 100;
                } 

                return new BaseResponse<GetTaskCompletionRateResponse>(true, "Completion rate retrieved successfully", response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_TASK_COMPLETION_RATE_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetTaskCompletionRateResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
