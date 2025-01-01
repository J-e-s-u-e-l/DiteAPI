using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Auth.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Utilities.Middleware
{
    public class JsonExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JsonExceptionMiddleware> _logger;

        public JsonExceptionMiddleware(RequestDelegate next, ILogger<JsonExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"JSON_EXCEPTION_MIDDLEWARE_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Status = false,
                    Message = "Invalid JSON format or value."
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
