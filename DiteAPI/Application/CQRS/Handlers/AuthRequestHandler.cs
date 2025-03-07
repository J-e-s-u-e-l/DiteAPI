using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Auth.JWT;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Application.CQRS.Handlers
{
    public class AuthRequestHandler : IRequestHandler<AuthRequest, BaseResponse<LoginResponse>>
    {
        private readonly DataDBContext _dbContext;
        private SignInManager<GenericUser> _signInManager;
        private UserManager<GenericUser> _userManager;
        private readonly ILogger<AuthRequestHandler> _logger;
        private readonly IJwtHandler _jwtHandler;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthRequestHandler(SignInManager<GenericUser> signInManager,
            UserManager<GenericUser> userManager,
            DataDBContext dBContext,
            ILogger<AuthRequestHandler> logger,
            IJwtHandler jwtHandler,
            IOptions<AppSettings> options,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbContext = dBContext;
            _jwtHandler = jwtHandler;
            _logger = logger;
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<LoginResponse>> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
             try
             {
                 var user = await _dbContext.GenericUser.Where(x => x.Email!.ToLower() == request.Email.ToLower()).FirstOrDefaultAsync(cancellationToken);

                 if (user == null)
                     return new BaseResponse<LoginResponse>(false, $"{_appSettings.UserWithEmailNotFound}");

                if (user.LockoutEnabled)
                {
                    return new BaseResponse<LoginResponse>(false, _appSettings.AccountLocked);
                }
                using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    /*var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, true);

                    if (!result.Succeeded)
                    {
                        if (result.IsLockedOut)
                        {
                            _logger?.LogInformation($"LOGIN_REQUEST => Process cancelled | Account is locked");

                            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(1000);
                            await _dbContext.SaveChangesAsync(cancellationToken);
                            await transaction.CommitAsync();
                            return new BaseResponse<LoginResponse>(false, $"{_appSettings.AccountLocked}");
                        }
                        else
                        {
                            //await _userManager.AccessFailedAsync(user);
                            int maxAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts;
                            int failedAttempts = user.AccessFailedCount;
                            if (maxAttempts - failedAttempts == 1)
                            {
                                user.LockoutEnabled = true;
                                user.LockoutEnd = DateTime.UtcNow.AddYears(1000);
                                await _dbContext.SaveChangesAsync(cancellationToken);
                                await transaction.CommitAsync();
                                return new BaseResponse<LoginResponse>(false, $"{_appSettings.AccountLocked}");
                            }

                            await _dbContext.SaveChangesAsync(cancellationToken);
                            await transaction.CommitAsync();
                            _logger.LogInformation($"LOGIN_REQUEST => Process cancelled | Invalid login credentials provided: \nRequest: {request}");
                            return new BaseResponse<LoginResponse>(false, $"Invalid login credentials provided. Please try again. You have {maxAttempts - 1 - failedAttempts} login attempt(s) remaining.");
                        }
                    }*/

                    var verifyPswd = await _userManager.CheckPasswordAsync(user, request.Password);

                    if (!verifyPswd)
                    {
                        await _userManager.AccessFailedAsync(user);
                        int maxAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts;
                        int failedAttempts = user.AccessFailedCount;
                        if (maxAttempts - failedAttempts == 1)
                        {
                            user.LockoutEnabled = true;
                            user.LockoutEnd = DateTime.UtcNow.AddYears(1000);
                            await _dbContext.SaveChangesAsync(cancellationToken);
                            await transaction.CommitAsync();
                            return new BaseResponse<LoginResponse>(false, $"{_appSettings.AccountLocked}");
                        }

                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();
                        _logger.LogInformation($"LOGIN_REQUEST => Process cancelled | Invalid login credentials provided: \nRequest: {request}");
                        return new BaseResponse<LoginResponse>(false, $"Invalid login credentials provided. Please try again. You have {maxAttempts - 1 - failedAttempts} login attempt(s) remaining.");
                    }

                    // reset lockout count
                    user.LockoutEnabled = false;
                    user.AccessFailedCount = 0;
                    user.LockoutEnd = DateTimeOffset.UtcNow;
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    if (!user.EmailConfirmed)
                    {
                        await transaction.CommitAsync();
                        return new BaseResponse<LoginResponse>(false, $"{_appSettings.EmailNotVerified}");
                    }

                    user.LastLogin = DateTime.UtcNow;
                    user.IsActive = true;
                    await _dbContext.SaveChangesAsync(cancellationToken);


                     var loginResponse = _jwtHandler.Create(new JwtRequest
                     {
                         UserId = user.Id,
                         Username = user.UserName,
                         EmailAddress = user.Email
                         //IsEmailVerified = user.EmailConfirmed,
                     });

                    await transaction.CommitAsync(cancellationToken);
                    _logger.LogInformation($"User signed in successfully at {DateTime.UtcNow}\nUser name: {user.UserName}\nUser ID: {user.Id}");
                    
                    var cokkieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        MaxAge = TimeSpan.FromMinutes(30),
                        Path = "/"
                    };

                    _httpContextAccessor.HttpContext.Response.Cookies.Append("authToken", loginResponse.Token, cokkieOptions);

                    return new BaseResponse<LoginResponse>(true, $"{_appSettings.SingInSuccessful}", loginResponse);
                 }
                 catch (Exception ex)
                 {
                    _logger.LogError($"Something went wrong:\n {ex.StackTrace}: {ex.Message}");
                    await transaction.RollbackAsync(cancellationToken);
                    return new BaseResponse<LoginResponse>(false, $"{_appSettings.ProcessingError}");
                }
             }
             catch (Exception ex)
             {
                _logger.LogError($"Something went wrong:\n {ex.StackTrace}: {ex.Message}");
                return new BaseResponse<LoginResponse>(false, $"{_appSettings.ProcessingError}");
             }
        }
    }
}
