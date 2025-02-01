using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.Infrastructure.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiteAPI.Infrastructure.Infrastructure.Auth.JWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                    await attachUserToContext(context, token);
                else
                {
                    token = context.Request.Query["access_token"];
                    if (token != null)
                        await attachUserToContext(context, token);
                    /*else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = "Authentication required. Please log in to access this resource." }));
                    }*/
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"JWT_MIDDLEWARE => Something went wrong\n{ex.StackTrace}: {ex.Message}");
            }
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "sub").Value);

                context.Items["UserId"] = userId;

                //context.WebSockets.AcceptWebSocketAsync();

                //context.User.Identities.First().AddClaim(new Claim("UserId", userId.ToString()));
                var identity = context.User.Identity as ClaimsIdentity;

                if (!identity.HasClaim(c => c.Type == "UserId"))
                {
                    identity.AddClaim(new Claim("UserId", userId.ToString()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"JWT_MIDDLEWARE => Something went wrong\n{ex.StackTrace}: {ex.Message}");
            }
        }
    }
}
