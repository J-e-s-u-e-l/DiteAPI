using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetMessageDetailsQueryHandler : IRequestHandler<GetMessageDetailsQuery, BaseResponse<GetMessageDetailsResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetMessageDetailsQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHelperMethods _helperMethods;

        public GetMessageDetailsQueryHandler(DataDBContext dbContext, ILogger<GetMessageDetailsQueryHandler> logger, IOptions<AppSettings> appSettings, IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _helperMethods = helperMethods;
        }

        public async Task<BaseResponse<GetMessageDetailsResponse>> Handle(GetMessageDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var messageDetails = await _dbContext.Messages
                    .Where(m => m.Id == request.MessageId)
                    .SelectMany(md => new
                    {
                        MessageTitle = md.MessageTitle,
                        MessageBody = md.MessageBody,
                        SenderUsername = md.Sender.UserName ?? "Unknown",
                        SenderRoleInAcademy = md.Sender.AcademyMembersRoles
                                                                    .Where(amr => amr.AcademyId == request.AcademyId)
                                                                    .Select(x => x.IdentityRole.Name)
                                                                    .FirstOrDefault() ?? "Unknown",
                        TrackName = md.Track?.TrackName,
                        SentAt = _helperMethods.ToAgoFormat(md.SentAt),
                        TotalNumberOfResponses = responseCounts.ContainsKey(message.Id) ? responseCounts[message.Id] : 0
                    })
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ACADEMY_DETAILS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetMessageDetailsResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
