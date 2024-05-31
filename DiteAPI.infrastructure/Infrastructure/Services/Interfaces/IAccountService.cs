using DiteAPI.infrastructure.Data.Entities;

namespace DiteAPI.infrastructure.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<OtpRequestResult>> SendOTPAsync(SendOTPToUser request, CancellationToken cancellationToken);
        Task<BaseResponse<VerificationLinkRequestResult>> SendVerificationLinkAsync(SendVerificationLinkToUser request, CancellationToken cancellationToken);
        Task<BaseResponse> SendWelcomeEmailAsync(SendWelcomeRequest request, string applicationName, CancellationToken cancellationToken);
        Task<BaseResponse> ValidateCodeAsync(ValidateCodeRequest otpRequest, CancellationToken cancellationToken);
    }
} 
