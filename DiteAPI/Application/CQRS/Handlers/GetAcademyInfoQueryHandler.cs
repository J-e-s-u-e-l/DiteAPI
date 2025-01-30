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
    public class GetAcademyInfoQueryHandler : IRequestHandler<GetAcademyInfoQuery, BaseResponse<GetAcademyInfoResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAcademyInfoQueryHandler> _logger;
        private readonly AppSettings _appSettings;

        public GetAcademyInfoQueryHandler(DataDBContext dbContext, ILogger<GetAcademyInfoQueryHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse<GetAcademyInfoResponse>> Handle(GetAcademyInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string? academyCode = await _dbContext.Academy
                                    .Where(a => a.Id == request.AcademyId)
                                    .Select(x => x.AcademyCode).FirstOrDefaultAsync();

                return academyCode == null
                    ? new BaseResponse<GetAcademyInfoResponse>(false, "Academy not found")
                    : new BaseResponse<GetAcademyInfoResponse>(true, "Academy info retrieved successfully", new GetAcademyInfoResponse { AcademyCode = academyCode});
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ACADEMY_INFO_QUERYHANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetAcademyInfoResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
