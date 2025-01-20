using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Data.Models;
using MediatR;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DiteAPI.Infrastructure.Data.Entities;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class ChangeAcademyMemberRoleCommandHandler : IRequestHandler<ChangeAcademyMemberRoleCommand, BaseResponse>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<ChangeAcademyMemberRoleCommandHandler> _logger;
        private readonly AppSettings _appSettings;

        public ChangeAcademyMemberRoleCommandHandler(DataDBContext dbContext, ILogger<ChangeAcademyMemberRoleCommandHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse> Handle(ChangeAcademyMemberRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                var memberCurrentRole = await _dbContext.AcademyMembersRoles
                    .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                    .Select(x => x.IdentityRole.NormalizedName).FirstOrDefaultAsync();

                // Update member role based on current role and new role
                switch ((memberCurrentRole, request.NewRoleName))
                {
                    case ("Member", "Admin"):
                        await _dbContext.AcademyMembersRoles
                            .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                            .ExecuteUpdateAsync(setter => setter
                            .SetProperty(column => column.RoleId, new Guid(_appSettings.AdminRoleId)));
                    break;

                    case ("Member", "Facilitator"):
                        // Remove existing role records of the member in this academy
                        await _dbContext.AcademyMembersRoles
                                .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                                .ExecuteDeleteAsync(cancellationToken);

                        var tracksFacilitatedByMember_Member = request.AssignedTracksIds.Select(assignedTrack => new AcademyMembersRoles
                        {
                            GenericUserId = request.MemberId,
                            AcademyId = request.AcademyId,
                            RoleId = new Guid(_appSettings.FacilitatorRoleId),
                            TrackId = assignedTrack
                        }).ToList();

                        // Persist DB with new records of tracks to be facilitated by this member
                        await _dbContext.AddRangeAsync(tracksFacilitatedByMember_Member, cancellationToken);
                    break;

                    case ("Admin", "Facilitator"):
                        // Remove existing role records of the member in this academy
                        await _dbContext.AcademyMembersRoles
                                .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                                .ExecuteDeleteAsync(cancellationToken);

                        var tracksFacilitatedByMember_Admin = request.AssignedTracksIds.Select(assignedTrack => new AcademyMembersRoles
                        {
                            GenericUserId = request.MemberId,
                            AcademyId = request.AcademyId,
                            RoleId = new Guid(_appSettings.FacilitatorRoleId),
                            TrackId = assignedTrack
                        }).ToList();

                        // Persist DB with new records of tracks to be facilitated by this member
                        await _dbContext.AddRangeAsync(tracksFacilitatedByMember_Admin, cancellationToken);
                    break;

                    case ("Admin", "Member"):
                        await _dbContext.AcademyMembersRoles
                            .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                            .ExecuteUpdateAsync(setter => setter
                            .SetProperty(column => column.RoleId, new Guid(_appSettings.MemberRoleId)));
                    break;

                    case ("Facilitator", "Admin"):
                        // Remove existing role records of the member in this academy
                        await _dbContext.AcademyMembersRoles
                                .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                                .ExecuteDeleteAsync(cancellationToken);

                        var addUpdatedMemberRole_Admin = new AcademyMembersRoles
                        {
                            GenericUserId = request.MemberId,
                            AcademyId = request.AcademyId,
                            RoleId = new Guid(_appSettings.AdminRoleId)
                        };

                        // Save the updated role for this member in the academy to the database
                        await _dbContext.AddAsync(addUpdatedMemberRole_Admin, cancellationToken);
                    break;

                    case ("Facilitator", "Member"):
                        // Remove existing role records of the member in this academy
                        await _dbContext.AcademyMembersRoles
                                .Where(am => am.GenericUserId == request.MemberId && am.AcademyId == request.AcademyId)
                                .ExecuteDeleteAsync(cancellationToken);

                        var addUpdatedMemberRole_Member = new AcademyMembersRoles
                        {
                            GenericUserId = request.MemberId,
                            AcademyId = request.AcademyId,
                            RoleId = new Guid(_appSettings.MemberRoleId)
                        };

                        // Save the updated role for this member in the academy to the database
                        await _dbContext.AddAsync(addUpdatedMemberRole_Member, cancellationToken);
                    break;

                    default:
                        // No action has been implemented yet for unhandled cases
                    break;
                }

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new BaseResponse(true, "The member's role has been successfully updated.");
            }

            catch (Exception ex)
            {
                _logger.LogError($"CHANGE_ACADEMY_MEMBER_ROLE_COMMAND_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, _appSettings.ProcessingError);
            }
        }
    }
}
