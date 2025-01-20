using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, BaseResponse<GetAllMembersResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllMembersQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllMembersQueryHandler(DataDBContext dbContext, ILogger<GetAllMembersQueryHandler> logger, IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<GetAllMembersResponse>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    bool isUserAnAdmin = await _dbContext.AcademyMembersRoles
                        .AnyAsync(x => 
                            x.GenericUserId == userId 
                            && x.AcademyId == request.AcademyId 
                            && x.RoleId == new Guid(_appSettings.AdminRoleId));

                    var academyMemberDetails = await _dbContext.AcademyMembersRoles
                        .Where(amd => amd.AcademyId == request.AcademyId)
                        .GroupBy(amd => amd.GenericUserId)
                        .Select(g => new AcademyMemberDetails
                        {
                            UserId = g.Key,
                            Username = g.Select(amd => amd.GenericUser.UserName).FirstOrDefault(),
                            RoleName = g.Any(amd => amd.IdentityRole.Name == "Facilitator") ? "Facilitator" : g.Select(amd => amd.IdentityRole.Name).FirstOrDefault(),
                            assignedTracks = g.Where(amd => amd.IdentityRole.Name == "Facilitator")     // Get track IDs for users with "Facilitator" role
                            .Select(amd => amd.TrackId)
                            .Where(trackId => trackId.HasValue)
                            .Select(trackId => trackId.Value)
                            .ToList()
                        }).ToListAsync();

                    GetAllMembersResponse getAllMembersResponse = new GetAllMembersResponse
                    {
                        IsAnAdminInTheAcademy = isUserAnAdmin,
                        Members = academyMemberDetails
                    };

                    return new BaseResponse<GetAllMembersResponse>(true, "Academy Members Details Successfully Fetched!", getAllMembersResponse);
                }

                catch (Exception ex)
                {
                    _logger.LogError($"GET_ALL_MEMBERS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<GetAllMembersResponse>(false, $"{_appSettings.ProcessingError}");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_MEMBERS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetAllMembersResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
