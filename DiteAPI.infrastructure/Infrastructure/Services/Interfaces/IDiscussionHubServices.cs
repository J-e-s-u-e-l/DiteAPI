using DiteAPI.infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface IDiscussionHubServices
    {
        Task<BaseResponse> JoinAcademy(string academyCode);
    }
}
