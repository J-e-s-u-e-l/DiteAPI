using DiteAPI.infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<OtpRequestResult>> SendOTPAsync(SendOTPRequest request, CancellationToken cancellationToken);
        Task<BaseResponse> ValidateOTPCodeAsync(ValidateOtpRequest otpRequest, CancellationToken cancellationToken);
    }
}
