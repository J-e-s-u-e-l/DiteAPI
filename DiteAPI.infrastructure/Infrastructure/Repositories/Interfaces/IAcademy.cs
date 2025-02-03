using DiteAPI.infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Repositories.Interfaces
{
    public interface IAcademy
    {
        Task<MessageDto> GetMessageDetails(Guid messageId);
    }
}
