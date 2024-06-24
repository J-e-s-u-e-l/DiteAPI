using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<SessionService> _logger;
        public SessionService(IHttpContextAccessor httpContextAccessor, ILogger<SessionService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public void SetStringInSession(string key, string value)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError($"SESSION_SERVICE => HttpContext is null while trying to set session value for key: UserId");
            }

            httpContext.Session.SetString(key, value);
        }

        public string GetStringFromSession(string key)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError($"SESSION_SERVICE => HttpContext is null while trying to get session value for key: UserId");
                return null!;
            }
            httpContext.Session.LoadAsync().Wait();

            return httpContext.Session.GetString(key)!;
        }
    }
}
