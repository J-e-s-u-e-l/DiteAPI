using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllTracksQueryHandler : IRequestHandler<GetAllTracksQuery, BaseResponse<IEnumerable<GetAllTracksResponse>>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllTracksQueryHandler> _logger;
        private readonly AppSettings _appSettings;

        public GetAllTracksQueryHandler(DataDBContext dbContext, ILogger<GetAllTracksQueryHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse<IEnumerable<GetAllTracksResponse>>> Handle(GetAllTracksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    bool academyExists = await _dbContext.Academy.AnyAsync(a => a.Id == request.AcademyId);

                    if (academyExists)
                    {
                        var getAllTracksResponse = await _dbContext.Tracks
                        .Where(x => x.AcademyId == request.AcademyId)
                        .Select(x => new GetAllTracksResponse { TrackId = x.Id, TrackName = x.TrackName })
                        .ToListAsync();

                        return new BaseResponse<IEnumerable<GetAllTracksResponse>>(true, "Tracks fetched successfully!", getAllTracksResponse);
                    }

                    return new BaseResponse<IEnumerable<GetAllTracksResponse>>(false, "Academy does not exist!");
                }

                catch (Exception ex)
                {
                    _logger.LogError($"GET_ALL_MEMBERS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<IEnumerable<GetAllTracksResponse>>(false, $"{_appSettings.ProcessingError}");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_MEMBERS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<IEnumerable<GetAllTracksResponse>>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
