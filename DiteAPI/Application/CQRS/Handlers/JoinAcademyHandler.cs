using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using DiteAPI.infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Config;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class JoinAcademyHandler : IRequestHandler<JoinAcademyCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<RegistrationCommand> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JoinAcademyHandler(DataDBContext dbContext, ILogger<RegistrationCommand> logger, IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse> Handle(JoinAcademyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    // Check if Academy exists
                    var academyId = await _dbContext.Academy.Where(x => x.AcademyCode == request.AcademyCode).Select(x => x.Id).FirstOrDefaultAsync();

                    if (academyId == null)
                        return new BaseResponse(false, _appSettings.AcademyNotFound);

                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    // Check if the user has already joined the Academy
                    if(await _dbContext.AcademyMembers.AnyAsync(x => x.GenericUserId == userId && x.AcademyId == academyId)) 
                        return new BaseResponse(false, _appSettings.UserAlreadyExistInAcademy);

                    // Add user to the Academy
                    var newAcademyMember = new AcademyMembers
                    {
                        GenericUserId = userId,
                        AcademyId = academyId,
                        RoleId = new Guid(_appSettings.MemberRoleId)
                    };
                    await _dbContext.AcademyMembers.AddAsync(newAcademyMember);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);

                    return new BaseResponse(true, _appSettings.AcademyEnrollmentSuccessful);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"JOIN_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CREATE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
