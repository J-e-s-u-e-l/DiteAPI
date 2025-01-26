using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DiteAPI.Infrastructure.Data.Entities;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, BaseResponse<GetAllMessagesResponse>>
    {

        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllMembersQueryHandler> _logger;
        private readonly AppSettings _appSettings;

        public GetAllMessagesQueryHandler(DataDBContext dbContext, ILogger<GetAllMembersQueryHandler> logger, IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
        }


        public async Task<BaseResponse<GetAllMessagesResponse>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    /*                    var messagesQuery = _dbContext.Messages
                                            .Where(x => x.AcademyId == request.AcademyId)
                                            .OrderByDescending(m => m.SentAt)
                                            .Select(async m => new
                                            {
                                                m.Id,
                                                m.MessageTitle,
                                                m.MessageBody,
                                                m.SentAt,
                                                m.TrackId,
                                                Sender = await _dbContext.GenericUser.FirstOrDefaultAsync(u => u.Id == m.SenderId),
                                                Role = await _dbContext.AcademyMembersRoles.FirstOrDefaultAsync(r => r.GenericUserId == m.SenderId && r.AcademyId == m.AcademyId),
                                                Track = await _dbContext.Tracks.FirstOrDefaultAsync(t => t.Id == m.TrackId)
                                            });*/


                    var messagesQuery = _dbContext.Messages
                        .Include(m => m.Sender)
                        .Include(m => m.Track)
                        .Include(m => m.Sender.AcademyMembersRoles.Where(r => r.AcademyId == request.AcademyId))
                        .Where(m => m.AcademyId == request.AcademyId)
                        .OrderByDescending(m => m.SentAt);


                    var totalMessages = await messagesQuery.CountAsync(cancellationToken);

                    var messages = await messagesQuery
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(async m => new GetAllMessagesResponse
                        {
                            MessageId = m.Id,
                            MessageTitle = m.MessageTitle,
                            MessageBody = m.MessageBody,
                            SenderUsername = m.Sender.UserName,
                            SenderRoleInAcademy = m.Sender.AcademyMembersRoles
                                                            .Where(r => r.AcademyId == request.AcademyId)
                                                            .Select(r => r.IdentityRole.Name)
                                                            .FirstOrDefault(),
                            TrackName = m.Track.TrackName,
                            SentAt = m.SentAt,
                        });
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
