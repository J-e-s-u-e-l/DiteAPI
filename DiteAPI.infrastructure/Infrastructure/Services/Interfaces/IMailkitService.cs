using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Interfaces
{
    public interface IMailkitService
    {
        Task<BaseResponse> SendEmailAsync(SingleEmailRequest request);
        Task<BaseResponse> SendEmailToMultipleRecipientsAsync(MultipleEmailRequest request);
    }
}
