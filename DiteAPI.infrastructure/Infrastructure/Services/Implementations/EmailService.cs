using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public Task<BaseResponse> CheckDisposableEmailAsync(string email)
        {

        }

        public Task<BaseResponse<EmailBodyResponse>> GetEmailBody(EmailBodyRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTemplateFileAsync(string folderName, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> SendEmailAsync(SingleEmailRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> SendMultipleEmailAsync(SingleEmailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
