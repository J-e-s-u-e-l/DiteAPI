using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class UniqueUsernameCheckRequestHandler : IRequestHandler<UniqueUsernameCheckRequest, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AuthRequestHandler> _logger;

        public UniqueUsernameCheckRequestHandler(DataDBContext dbContext, IOptions<AppSettings> options, ILogger<AuthRequestHandler> logger)
        {
            _dbContext = dbContext;
            _appSettings = options.Value;
            _logger = logger;
        }

        async Task<BaseResponse> IRequestHandler<UniqueUsernameCheckRequest, BaseResponse>.Handle(UniqueUsernameCheckRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var isUsernameUnique = await _dbContext.GenericUser.AnyAsync(u => u.UserName == request.Username);
                return new BaseResponse(isUsernameUnique, "");
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong:\n {ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
