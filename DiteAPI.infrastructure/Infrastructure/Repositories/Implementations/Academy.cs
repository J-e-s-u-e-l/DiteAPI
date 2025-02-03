using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Repositories.Implementations
{
    public class Academy : IAcademy
    {
        private readonly DataDBContext _dbContext;
        private readonly IHelperMethods _helperMethods;

        public Academy(DataDBContext dbContext, IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _helperMethods = helperMethods;
        }

        public async Task<MessageDto> GetMessageDetails(Guid messageId)
        {
            // Fetch messages with sender and track details
            var messages = await _dbContext.Messages
                .Include(m => m.Sender)
                    .ThenInclude(s => s.AcademyMembersRoles)
                        .ThenInclude(amr => amr.IdentityRole)
                .Include(m => m.Track)
                .Include(m => m.Academy)
                .Where(m => m.Id == messageId)
                .ToListAsync();

            // Bulk-fetch response counts
            var responseCounts = await _dbContext.Messages
                .Where(m => m.Id == messageId)
                .GroupBy(m => m.ParentId)
                .Select(g => new { ParentId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.ParentId.Value, g => g.Count);

            // Map to DTOs
            /*var messageDto = messages.Select(message => new MessageDto
            {
                MessageId = message.Id,
                MessageTitle = message.MessageTitle,
                MessageBody = message.MessageBody,
                SenderUserName = message.Sender?.UserName ?? "Unkown",
                SenderRoleInAcademy = message.Sender?.AcademyMembersRoles
                                            .Where(amr => amr.AcademyId == message.AcademyId)
                                            .Select(x => x.IdentityRole.Name)
                                            .FirstOrDefault() ?? "Unkown",
                TrackName = message.Track?.TrackName,
                SentAt = _helperMethods.ToAgoFormat(message.SentAt),
                TotalNumberOfResponses = responseCounts.ContainsKey(message.Id) ? responseCounts[message.Id] : 0
            }).ToList();*/
            var messageDto = messages.Select(message => new MessageDto
            {
                MessageId = message.Id,
                MessageTitle = message.MessageTitle,
                MessageBody = message.MessageBody,
                SenderUserName = message.Sender?.UserName ?? "Unkown",
                SenderRoleInAcademy = message.Sender?.AcademyMembersRoles
                              .Where(amr => amr.AcademyId == message.AcademyId)
                              .Select(x => x.IdentityRole.Name)
                              .FirstOrDefault() ?? "Unkown",
                TrackName = message.Track?.TrackName,
                SentAt = _helperMethods.ToAgoFormat(message.SentAt),
                TotalNumberOfResponses = responseCounts.ContainsKey(message.Id) ? responseCounts[message.Id] : 0
            }).ToList();

            return messageDto;
        }
    }
}
