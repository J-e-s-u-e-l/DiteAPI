using DiteAPI.Infrastructure.Config;
using DiteAPI.infrastructure.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace DiteAPI.Infrastructure.Infrastructure.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Items["UserId"];
            if (userId == null)
                context.Result = new JsonResult(new { message = "Authentication required. Please log in to access this resource." }) { StatusCode = StatusCodes.Status401Unauthorized };    
        }
    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)
/*
    public class SignalRCustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override bool UserAuthorized()
        {
            return false;
            var userId = context.HttpContext.Items["UserId"];
            Console.WriteLine($"User id = {userId}");
            if (userId == null)
                context.Result = new JsonResult(new { message = "Authentication required. Please log in to access this resource." }) { StatusCode = StatusCodes.Status401Unauthorized };    
        }
    }*/
}
