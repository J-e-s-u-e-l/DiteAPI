using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Infrastructure.Infrastructure.Repositories.Implementations
{
    public class AcademyRepository : IAcademyRepository
    {
        private readonly DataDBContext _dbContext;
        private readonly IHelperMethods _helperMethods;

        public AcademyRepository(DataDBContext dbContext, IHelperMethods helperMethods)
        {
            _dbContext = dbContext;
            _helperMethods = helperMethods;
        }

        public async Task<List<MessageDto>> GetMessageDetailsAsync(List<Guid> messageIds)
        {
            // Fetch messages with sender and track details
            var messages = await _dbContext.Messages
                .Include(m => m.Sender)
                    .ThenInclude(s => s.AcademyMembersRoles)
                        .ThenInclude(amr => amr.IdentityRole)
                .Include(m => m.Track)
                .Include(m => m.Academy)
                .Where(m => messageIds.Contains(m.Id))
                .ToListAsync();

            // Bulk-fetch response counts
            var responseCounts = await _dbContext.Messages
                .Where(m => messageIds.Contains(m.Id))
                .GroupBy(m => m.ParentId)
                .Select(g => new { ParentId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.ParentId.Value, g => g.Count);

            return messages.Select(message => _helperMethods.MapToMessageDto(message, responseCounts)).ToList();
        }
    }
}
