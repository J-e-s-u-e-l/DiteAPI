using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DiteAPI.Infrastructure.Data.Entities;
using System.Collections.Generic;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, BaseResponse<GetAllMessagesResponse>>
    {

        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllMembersQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHelperMethods _helperMethods;
        private readonly IAcademyRepository _academyRepository;

        public GetAllMessagesQueryHandler(DataDBContext dbContext, ILogger<GetAllMembersQueryHandler> logger, IOptions<AppSettings> options, IHelperMethods helperMethods, IAcademyRepository academyRepository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _helperMethods = helperMethods;
            _academyRepository = academyRepository;
        }


        public async Task<BaseResponse<GetAllMessagesResponse>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var totalCountOfMessagesInAcademy = await _dbContext.Messages
                        .Where(m => m.AcademyId == request.AcademyId)
                        .CountAsync();


                    // Get paginated message IDs
                    var messageIds = await _dbContext.Messages
                        .Include(m => m.Sender)
                        .ThenInclude(s => s.AcademyMembersRoles)
                        .ThenInclude(amr => amr.IdentityRole)
                        .Where(m => m.AcademyId == request.AcademyId)
                        .OrderByDescending(m => m.SentAt)
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(m => m.Id)
                        .ToListAsync();

                    var messageDtos = await _academyRepository.GetMessageDetailsAsync(messageIds);


                    /*
                    // Fetch messages with sender and track details
                    var messages = await _dbContext.Messages
                        .Include(m => m.Sender)
                            .ThenInclude(s => s.AcademyMembersRoles)
                                .ThenInclude(amr => amr.IdentityRole)
                        .Include(m => m.Track)
                        .Where(m => messageIds.Contains(m.Id))
                        .ToListAsync();

                    // Bulk-fetch response counts
                    var responseCounts = await _dbContext.Messages
                        .Where(m => messageIds.Contains(m.ParentId.Value))
                        .GroupBy(m => m.ParentId)
                        .Select(g => new { ParentId = g.Key, Count = g.Count() })
                        .ToDictionaryAsync(g => g.ParentId.Value, g => g.Count);

                    // Map to DTOs
                    var messageDtos = messages.Select(message => new MessageDto
                    {
                        MessageId = message.Id,
                        MessageTitle = message.MessageTitle,
                        MessageBody = message.MessageBody,
                        SenderUserName = message.Sender?.UserName ?? "Unkown",
                        SenderRoleInAcademy = message.Sender?.AcademyMembersRoles
                                                    .Where(amr => amr.AcademyId == request.AcademyId)
                                                    .Select(x => x.IdentityRole.Name)
                                                    .FirstOrDefault() ?? "Unkown",
                        TrackName = message.Track?.TrackName,
                        SentAt = _helperMethods.ToAgoFormat(message.SentAt),
                        TotalNumberOfResponses = responseCounts.ContainsKey(message.Id) ? responseCounts[message.Id] : 0
                    }).ToList(); */


                    var remainingMessagesCount = totalCountOfMessagesInAcademy - (request.PageNumber * request.PageSize);
                    if (remainingMessagesCount < 0)
                    {
                        remainingMessagesCount = 0;
                    }

                    var response = new GetAllMessagesResponse
                    {
                        RemainingMessagesCount = remainingMessagesCount,
                        Messages = messageDtos
                    };

                    return new BaseResponse<GetAllMessagesResponse>(true, "Academy messages retrieved succesfully", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"GET_ALL_MESSAGES_HANDLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<GetAllMessagesResponse>(false, $"{_appSettings.ProcessingError}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_MESSAGES_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<GetAllMessagesResponse>(false, $"{_appSettings.ProcessingError}");
            }
        }

    }
}
