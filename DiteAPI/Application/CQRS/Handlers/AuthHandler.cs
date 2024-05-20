using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Auth.JWT;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class AuthHandler : IRequestHandler<AuthRequest, BaseResponse<LoginResponse>>
    {
        private readonly DataDBContext _dbContext;
        private SignInManager<GenericUser> _signInManager;
        private UserManager<GenericUser> _userManager;
        private readonly ILogger<AuthHandler> _logger;
        private readonly IJwtHandler _jwtHandler;

        public AuthHandler(SignInManager<GenericUser> signInManager,
            UserManager<GenericUser> userManager,
            DataDBContext dBContext,
            ILogger<AuthHandler> logger,
            IJwtHandler jwtHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dBContext;
            _jwtHandler = jwtHandler;
            _logger = logger;
        }

        public async Task<BaseResponse<LoginResponse>> Handle(AuthRequest request, CancellationToken cancellationToken)
         {
             try
             {
                 var user = await _dbContext.GenericUser.Where(x => x.Email.ToLower() == request.Email.ToLower()).FirstOrDefaultAsync(cancellationToken);

                 if (user == null)
                 {
                     return new BaseResponse<LoginResponse>(false, "A user tied to this email was not found");
                 }
                 var verifyPswd = await _userManager.CheckPasswordAsync(user, request.Password);

                 using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                 try
                 {
                     var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);

                     if (!result.Succeeded)
                     {
                         if (result.IsLockedOut)
                         {
                             _logger?.LogInformation($"LOGIN_REQUEST => Process cancelled | Account is locked");

                             user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(1000);
                             await _dbContext.SaveChangesAsync(cancellationToken);
                             await transaction.CommitAsync();
                             return new BaseResponse<LoginResponse>(false, "Your account has been locked. To regain access, please initiate a password reset to unlock your account.");
                         }
                         else
                         {
                             await _userManager.AccessFailedAsync(user);
                             int maxAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts;
                             int failedAttempts = user.AccessFailedCount;
                             if (maxAttempts - failedAttempts == 1)
                             {
                                 user.LockoutEnabled = true;
                                 user.LockoutEnd = DateTime.UtcNow.AddYears(1000);
                                 await _dbContext.SaveChangesAsync(cancellationToken);
                                 return new BaseResponse<LoginResponse>(false, $"Invalid login credentials provided. Please try again. You have {maxAttempts - 1 - failedAttempts} login attempts remaining.");
                             }
                         }
                     }

                     // reset lockout count
                     user.LockoutEnabled = false;
                     user.AccessFailedCount = 0;
                     user.LockoutEnd = DateTimeOffset.UtcNow;
                     await _dbContext.SaveChangesAsync(cancellationToken);

                     if (!user.EmailConfirmed)
                     {
                         await transaction.CommitAsync();
                         return new BaseResponse<LoginResponse>(false, $"Your account has not been verified. Please proceed to verify your account.");
                     }

                     var loginResponse = _jwtHandler.Create(new JwtRequest
                     {
                         UserId = user.Id,
                         FirstName = user.FirstName,
                         LastName = user.LastName,
                         MiddleName = user.MiddleName,
                         EmailAddress = user.Email,
                         IsEmailVerified = user.EmailConfirmed,
                     });
                    await transaction.CommitAsync(cancellationToken);
                    _logger.LogInformation($"User signed in successfully at {DateTime.UtcNow}\nUser name: {user.UserName}\nUser ID: {user.Id}");
                    return new BaseResponse<LoginResponse>(true, "Sign-in Successful", loginResponse);
                 }
                 catch (Exception ex)
                 {
                    _logger.LogError($"Something went wrong:\n {ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken);
                    return new BaseResponse<LoginResponse>(false, "An error occurred while processing your login request.");
                }
             }
             catch (Exception ex)
             {
                _logger.LogError($"Something went wrong:\n {ex.StackTrace}: {ex.Message}");
                return new BaseResponse<LoginResponse>(false, "An error occurred while processing your login request.");
             }
        }
    }
}
