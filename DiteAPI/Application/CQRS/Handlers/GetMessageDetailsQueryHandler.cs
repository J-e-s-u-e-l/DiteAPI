using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using DiteAPI.Infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetMessageDetailsQueryHandler : IRequestHandler<GetMessageDetailsQuery, BaseResponse<GetMessageDetailsResponse>>
    {
        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetMessageDetailsQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IAcademyRepository _academyRepository;
        private readonly IHelperMethods _helperMethods;

        public GetMessageDetailsQueryHandler(DataDBContext dbContext, ILogger<GetMessageDetailsQueryHandler> logger, IOptions<AppSettings> appSettings, IAcademyRepository academyRepository, IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = appSettings.Value;
            _academyRepository = academyRepository;
            _helperMethods = helperMethods;
        }

        public async Task<BaseResponse<GetMessageDetailsResponse>> Handle(GetMessageDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _academyRepository.GetMessageDetailsAsync(new List<Guid> { request.MessageId });

                var responsesToMessage = await _dbContext.Messages
                                                .Where(r => r.ParentId == request.MessageId)
                                                .Select(res => new ResponseDto
                                                {
                                                    ResponseId = res.Id,
                                                    ResponseBody = res.MessageBody,
                                                    ResponderUsername = res.Sender.UserName,
                                                    ResponderRoleInAcademy = res.Sender.AcademyMembersRoles.Select(x => x.IdentityRole.Name).FirstOrDefault(),
                                                    SentAtAgo = _helperMethods.ToAgoFormat(res.SentAt),
                                                    SentAt = res.SentAt
                                                }).ToListAsync();

                return new BaseResponse<GetMessageDetailsResponse>(true, "Responses to message retrieved successfully", new GetMessageDetailsResponse { Message = message, Responses = responsesToMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ACADEMY_DETAILS_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetMessageDetailsResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
