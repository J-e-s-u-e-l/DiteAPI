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

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, BaseResponse<GetAllMessagesResponse>>
    {

        private readonly DataDBContext _dbContext;
        private readonly ILogger<GetAllMembersQueryHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHelperMethods _helperMethods;

        public GetAllMessagesQueryHandler(DataDBContext dbContext, ILogger<GetAllMembersQueryHandler> logger, IOptions<AppSettings> options, IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _logger = logger;
            _appSettings = options.Value;
            _helperMethods = helperMethods;
        }


        public async Task<BaseResponse<GetAllMessagesResponse>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    /*var response = await _dbContext.Messages
                        .Include(m => m.Sender)
                        .ThenInclude(s => s.AcademyMembersRoles)
                        .ThenInclude(amr => amr.IdentityRole)
                        .Where(m => m.AcademyId == request.AcademyId)
                        .OrderByDescending(m => m.SentAt)
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(message => new GetAllMessagesResponse
                        {
                            MessageId = message.Id,
                            MessageTitle = message.MessageTitle,
                            MessageBody = message.MessageBody,
                            SenderUsername = message.Sender.UserName ?? "Unknown",
                            SenderRoleInAcademy = message.Sender.AcademyMembersRoles
                                                                    .Where(amr => amr.AcademyId == request.AcademyId)
                                                                    .Select(x => x.IdentityRole.Name)
                                                                    .FirstOrDefault() ?? "Unknown",
                            TrackName = message.Track.TrackName ?? "General",
                            SentAt = message.SentAt
                        })
                        .ToListAsync();*/
                    var totalCountOfMessagesInAcademy = await _dbContext.Messages
                        .Where(m => m.AcademyId == request.AcademyId)
                        .CountAsync();

                    var messagesInTheAcademy = await _dbContext.Messages
                        .Include(m => m.Sender)
                        .ThenInclude(s => s.AcademyMembersRoles)
                        .ThenInclude(amr => amr.IdentityRole)
                        .Where(m => m.AcademyId == request.AcademyId)
                        .OrderByDescending(m => m.SentAt)
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .Select(message => new 
                        {
                            MessageId = message.Id,
                            MessageTitle = message.MessageTitle,
                            MessageBody = message.MessageBody,
                            SenderUsername = message.Sender.UserName ?? "Unknown",
                            SenderRoleInAcademy = message.Sender.AcademyMembersRoles
                                                                    .Where(amr => amr.AcademyId == request.AcademyId)
                                                                    .Select(x => x.IdentityRole.Name)
                                                                    .FirstOrDefault() ?? "Unknown",
                            TrackName = message.Track.TrackName ?? "General",
                            SentAt = message.SentAt
                        })
                        .ToListAsync();

                    var remainingMessagesCount = totalCountOfMessagesInAcademy - (request.PageNumber * request.PageSize);
                    if (remainingMessagesCount < 0)
                    {
                        remainingMessagesCount = 0;
                    }

                    var response = new GetAllMessagesResponse
                    {
                        RemainingMessagesCount = remainingMessagesCount,
                        Messages = messagesInTheAcademy.Select(msg => new MessageDto
                        {
                            MessageId = msg.MessageId,
                            MessageTitle = msg.MessageTitle,
                            MessageBody = msg.MessageBody,
                            SenderUserName = msg.SenderUsername,
                            SenderRoleInAcademy = msg.SenderRoleInAcademy,
                            TrackName = msg.TrackName,
                            SentAt = _helperMethods.ToAgoFormat(msg.SentAt)
                        }).ToList()
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
