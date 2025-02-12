using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Interfaces
{
    public interface IEmailService
    {
        //Tasks<BaseResponse> CheckDisposableEmailAsync(string email);
        Task<BaseResponse> SendEmailAsync(SingleEmailRequest request);
        Task<BaseResponse> SendMultipleEmailAsync(MultipleEmailRequest request);
        Task<string> GetTemplateFileAsync(string folderName, string fileName);
        Task<BaseResponse<EmailBodyResponse>> GetEmailBody(EmailBodyRequest request);
    }
}
