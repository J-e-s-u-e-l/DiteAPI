using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using DiteAPI.infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Data.Entities;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class JoinAcademyHandler : IRequestHandler<JoinAcademyCommand, BaseResponse<JoinAcademyResponse>>
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

        public async Task<BaseResponse<JoinAcademyResponse>> Handle(JoinAcademyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    // Check if Academy exists
                    var academyId = await _dbContext.Academy.Where(x => x.AcademyCode == request.AcademyCode).Select(x => x.Id).FirstOrDefaultAsync();

                    if (academyId == Guid.Empty)
                        return new BaseResponse<JoinAcademyResponse>(false, _appSettings.AcademyNotFound);

                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    // Check if the user has already joined the Academy
                    if(await _dbContext.AcademyMembers.AnyAsync(x => x.GenericUserId == userId && x.AcademyId == academyId)) 
                        return new BaseResponse<JoinAcademyResponse>(false, _appSettings.UserAlreadyExistInAcademy);

                    // Add user to the Academy
                    var newAcademyMember = new AcademyMembers
                    {
                        GenericUserId = userId,
                        AcademyId = academyId,
                        //RoleId = new Guid(_appSettings.MemberRoleId)
                    };

                    var newAcademyMemberRole = new AcademyMembersRoles
                    {
                        GenericUserId = userId,
                        AcademyId = academyId,
                        RoleId = new Guid(_appSettings.MemberRoleId)
                    };

                    await _dbContext.AcademyMembersRoles.AddAsync(newAcademyMemberRole, cancellationToken);
                    await _dbContext.AcademyMembers.AddAsync(newAcademyMember);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync(cancellationToken);

                    JoinAcademyResponse joinAcademyResponse = new JoinAcademyResponse
                    {
                        AcademyId = academyId
                    };

                    return new BaseResponse<JoinAcademyResponse>(true, _appSettings.AcademyEnrollmentSuccessful, joinAcademyResponse);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"JOIN_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse<JoinAcademyResponse>(false, _appSettings.ProcessingError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CREATE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<JoinAcademyResponse>(false, _appSettings.ProcessingError);
            }
        }
    }
}
