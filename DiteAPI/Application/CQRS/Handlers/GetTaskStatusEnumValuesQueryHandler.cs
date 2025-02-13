using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetTaskStatusEnumValuesQueryHandler : IRequestHandler<GetTaskStatusEnumValuesQuery, BaseResponse<GetTaskStatusEnumValuesResponse>>
    {
        private readonly ILogger<GetTaskStatusEnumValuesQueryHandler> _logger;
        private readonly AppSettings _appSettings;

        public GetTaskStatusEnumValuesQueryHandler(ILogger<GetTaskStatusEnumValuesQueryHandler> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<BaseResponse<GetTaskStatusEnumValuesResponse>> Handle(GetTaskStatusEnumValuesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetTaskStatusEnumValuesResponse
                {
                    TaskStatuses = Enum.GetValues(typeof(TaskStatusEnum))
                                        .Cast<TaskStatusEnum>()
                                        .Select(status => EnumHelper.GetDescription(status))
                                        .ToList()
                };

                return new BaseResponse<GetTaskStatusEnumValuesResponse>(true, "Task-Status Enum Values fetched successfully.", response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_TASK_STATUS_ENUM_VALUES_QUERY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetTaskStatusEnumValuesResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
