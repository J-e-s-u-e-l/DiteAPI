using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class LeaveAcademyCommandHandler : IRequestHandler<LeaveAcademyCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<LeaveAcademyCommandHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveAcademyCommandHandler(DataDBContext dbContext, ILogger<LeaveAcademyCommandHandler> logger, IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> Handle(LeaveAcademyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    bool academyExists = await _dbContext.Academy.AnyAsync(a => a.Id == request.AcademyId);

                    if (academyExists)
                    {
                        var memberToRemove = await _dbContext.AcademyMembers.FirstOrDefaultAsync(x => x.GenericUserId == userId && x.AcademyId == request.AcademyId);
                        //var memberToRemove = await _dbContext.GenericUser.FirstOrDefaultAsync(x => x.Id == userId);

                        if (memberToRemove != null)
                        {
                            //_dbContext.AcademyMembers.Remove(memberToRemove);
                            _dbContext.AcademyMembers.Remove(memberToRemove);
                            await _dbContext.SaveChangesAsync();
                            await transaction.CommitAsync(cancellationToken);
                        }

                        return new BaseResponse(true, "You have successfully left the academy.");
                    }

                    return new BaseResponse(false, "Academy does not exist!");
                }

                catch (Exception ex)
                {
                    _logger.LogError($"LEAVE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    return new BaseResponse(false, $"{_appSettings.ProcessingError}");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"LEAVE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
