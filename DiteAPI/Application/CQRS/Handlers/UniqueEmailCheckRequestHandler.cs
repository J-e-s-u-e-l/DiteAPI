using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class UniqueEmailCheckRequestHandler : IRequestHandler<UniqueEmailCheckRequest, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AuthRequestHandler> _logger;

        public UniqueEmailCheckRequestHandler(DataDBContext dbContext, IOptions<AppSettings> options, ILogger<AuthRequestHandler> logger)
        {
            _dbContext = dbContext;
            _appSettings = options.Value;
            _logger = logger;
        }

        async Task<BaseResponse> IRequestHandler<UniqueEmailCheckRequest, BaseResponse>.Handle(UniqueEmailCheckRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var isEmailUnique = await _dbContext.GenericUser.AnyAsync(u => u.Email == request.Email);
                return new BaseResponse(isEmailUnique, "");
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong:\n {ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
