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
    public class GetAcademyDetailsQueryHandler : IRequestHandler<GetAcademyDetailsQuery, BaseResponse<GetAcademyDetailsResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<SendOtpCommandHandler> _logger;
        private readonly AppSettings _appSettings;

        public GetAcademyDetailsQueryHandler(DataDBContext dbContext, ILogger<SendOtpCommandHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }

        public async Task<BaseResponse<GetAcademyDetailsResponse>> Handle(GetAcademyDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var academyDetails = await _dbContext.Academy.Where(x => x.Id.Equals(request.AcademyId)).Select(x => x.AcademyName).FirstOrDefaultAsync();

                    if (academyDetails == null)
                    {
                        return new BaseResponse<GetAcademyDetailsResponse>(false, "Academy Not Found");
                    }

                    GetAcademyDetailsResponse response = new GetAcademyDetailsResponse()
                    {
                        AcademyName = academyDetails
                    };

                    return new BaseResponse<GetAcademyDetailsResponse>(true, "Academy details retrieved successfully", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"SEND_OTP_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<GetAcademyDetailsResponse>(false, $"{_appSettings.ProcessingError}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"SEND_OTP_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetAcademyDetailsResponse>(false, $"{_appSettings.ProcessingError}");
            }

        }
    }
}
