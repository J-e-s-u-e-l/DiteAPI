using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class RemoveMemberFromAcademyCommandHanlder : IRequestHandler<RemoveMemberFromAcademyCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<RemoveMemberFromAcademyCommandHanlder> _logger;
        private readonly AppSettings _appSettings;

        public RemoveMemberFromAcademyCommandHanlder(DataDBContext dbContext, ILogger<RemoveMemberFromAcademyCommandHanlder> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(RemoveMemberFromAcademyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    //var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    bool academyExists = await _dbContext.Academy.AnyAsync(a => a.Id == request.AcademyId);

                    if (!academyExists)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new BaseResponse(false, "Academy does not exist!");
                    }
                    else
                    {
                        var memberToRemove = await _dbContext.AcademyMembers.FirstOrDefaultAsync(x => x.GenericUserId == request.MemberId && x.AcademyId == request.AcademyId);
                        var usernameOfMemberToRemove = await _dbContext.GenericUser.Where(a => a.Id == request.MemberId).Select(x => x.UserName).FirstOrDefaultAsync();
                        if (memberToRemove == null)
                        {
                            return new BaseResponse(false, "This member is not enrolled in the academy.");
                        }

                        var revokeExistingRolesOfMember = await _dbContext.AcademyMembersRoles.FirstOrDefaultAsync(x => x.GenericUserId == request.MemberId && x.AcademyId == request.AcademyId);

                        _dbContext.AcademyMembersRoles.Remove(revokeExistingRolesOfMember);
                        _dbContext.AcademyMembers.Remove(memberToRemove);

                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync(cancellationToken);

                        return new BaseResponse(true, $"{usernameOfMemberToRemove} has been removed from the Academy successfully.");
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogError($"LEAVE_ACADEMY_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
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
