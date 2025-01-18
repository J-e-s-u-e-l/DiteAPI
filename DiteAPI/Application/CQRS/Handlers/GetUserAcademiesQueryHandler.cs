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
    public class GetUserAcademiesQueryHandler : IRequestHandler<GetUserAcademiesQuery, BaseResponse<IEnumerable<GetUserAcademiesResponse>>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetUserAcademiesQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserAcademiesQueryHandler(DataDBContext dBContext, ILogger<GetUserAcademiesQueryHandler> logger, IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dBContext;
            _logger = logger;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<IEnumerable<GetUserAcademiesResponse>>> Handle(GetUserAcademiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = (Guid)_httpContextAccessor.HttpContext!.Items["UserId"]!;

                    // Fetch all academies the user is a member of
                    /*var userAcademies = await (from academyMembers in _dbContext.AcademyMembers
                                               where academyMembers.GenericUserId == userId
                                               group academyMembers by academyMembers.AcademyId into groupedAcademies
                                               select new
                                               {
                                                   AcademyId = groupedAcademies.Key,
                                                   MemberCount = groupedAcademies.Count()
                                               }).ToListAsync();*/
                    var userAcademies = await _dbContext.AcademyMembers
                        .Where(am => am.GenericUserId == userId)
                        .GroupBy(am => am.AcademyId)
                        .Select(groupedAcademies => new
                        {
                            AcademyId = groupedAcademies.Key,
                            MembersCount = _dbContext.AcademyMembers.Count(am => am.AcademyId == groupedAcademies.Key)
                        })
                        .ToListAsync();

                    // Fetch academy details for the academies the user is a member of
                    var academyDetails = await (from academy in _dbContext.Academy
                                                where userAcademies.Select(ua => ua.AcademyId).Contains(academy.Id)
                                                select new
                                                {
                                                    AcademyId = academy.Id,
                                                    AcademyName = academy.AcademyName,
                                                    AcademyDescription = academy.Description,
                                                    CreatorId = academy.CreatorId,
                                                }).ToListAsync();

                    // Fetch track counts for each academy
                    var trackCounts = await (from track in _dbContext.Tracks
                                             where userAcademies.Select(ua => ua.AcademyId).Contains(track.AcademyId)
                                             group track by track.AcademyId into groupedTracks
                                             select new
                                             {
                                                 AcademyId = groupedTracks.Key,
                                                 TracksCount = groupedTracks.Count()
                                             }).ToListAsync();

                    // Combine all the data into the final response
                    var response = (from ua in userAcademies
                                    join ad in academyDetails on ua.AcademyId equals ad.AcademyId
                                    join tc in trackCounts on ua.AcademyId equals tc.AcademyId into tcGroup
                                    from tc in tcGroup.DefaultIfEmpty()
                                    select new GetUserAcademiesResponse
                                    {
                                        AcademyId = ua.AcademyId,
                                        AcademyName = ad.AcademyName,
                                        AcademyDescription = ad.AcademyDescription,
                                        AcademyMembersCount = ua.MembersCount.ToString(),
                                        AcademyTracksCount = (tc?.TracksCount ?? 0).ToString(),
                                        AcademyCreatedByUser = ad.CreatorId == userId
                                    }).ToList();

                    /*var userAcademies = await (from academyMembers in _dbContext.AcademyMembers
                                               where academyMembers.GenericUserId == userId // Filter academies the user is a member of
                                               group academyMembers by academyMembers.AcademyId into groupedAcademies
                                               join academy in _dbContext.Academy on groupedAcademies.Key equals academy.Id
                                               select new
                                               {
                                                   AcademyId = groupedAcademies.Key,
                                                   AcademyName = academy.AcademyName,
                                                   AcademyDescription = academy.Description,
                                                   AcademyMembersCount = groupedAcademies.Count(),
                                                   AcademyTracksCount = _dbContext.Tracks.Count(t => t.AcademyId == groupedAcademies.Key),
                                                   AcademyCreatedByUser = academy.CreatorId == userId
                                               }).ToListAsync();

                    var response = userAcademies.Select(a => new GetUserAcademiesResponse
                    {
                        AcademyId = a.AcademyId,
                        AcademyName = a.AcademyName,
                        AcademyDescription = a.AcademyDescription,
                        AcademyMembersCount = a.AcademyMembersCount.ToString(),
                        AcademyTracksCount = a.AcademyTracksCount.ToString(),
                        AcademyCreatedByUser = a.AcademyCreatedByUser
                    }).ToList();*/

                    return new BaseResponse<IEnumerable<GetUserAcademiesResponse>>(true, "Academies successfully retrieved", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"GET_USER_ACADEMIES_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<IEnumerable<GetUserAcademiesResponse>>(false, $"{_appSettings.ProcessingError}");
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError($"GET_USER_ACADEMIES_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<IEnumerable<GetUserAcademiesResponse>>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
