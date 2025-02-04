using DiteAPI.infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces
{
    public interface IAcademyRepository
    {
        Task <List<MessageDto>> GetMessageDetailsAsync(List<Guid> messageId);
    }
}