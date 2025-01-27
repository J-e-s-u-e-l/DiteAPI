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

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, BaseResponse<List<GetAllMessagesResponse>>>
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


        public async Task<BaseResponse<List<GetAllMessagesResponse>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var response = await _dbContext.Messages
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
                        .ToListAsync();

                    return new BaseResponse<List<GetAllMessagesResponse>>(true, "Academy messages retrieved succesfully", response);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"GET_ALL_MESSAGES_HANDLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                    return new BaseResponse<List<GetAllMessagesResponse>>(false, $"{_appSettings.ProcessingError}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"GET_ALL_MESSAGES_HANDLER => Something went wrong\n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse<List<GetAllMessagesResponse>>(false, $"{_appSettings.ProcessingError}");
            }
        }

    }
}
