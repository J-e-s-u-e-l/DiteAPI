using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly DataDBContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IHelperMethods _helperMethods;


        public Task<BaseResponse<OtpRequestResult>> SendOTPAsync(SendOTPRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> ValidateOTPCodeAsync(ValidateOtpRequest otpRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
