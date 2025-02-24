using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Auth;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Infrastructure.Auth;
using DiteAPI.Infrastructure.Infrastructure.Auth.JWT;
using DiteAPI.Infrastructure.Infrastructure.Hubs;
using DiteAPI.Infrastructure.Infrastructure.Utilities.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DiteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", true);
            builder.Services.RegisterApplication();
            builder.Services.RegisterPersistence(builder.Configuration);
            builder.Services.RegisterIdentity();
            builder.Services.RegisterJwt(builder.Configuration);
            builder.Services.RegisterContactInformation(builder.Configuration);
            builder.Services.RegisterAppSettings(builder.Configuration);
            builder.Services.RegisterMailKitSection(builder.Configuration);
            builder.Services.RegisterEmailVerification(builder.Configuration);
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            builder.Services.AddSignalR();
            builder.Services.AddHttpContextAccessor();
            builder.Services.RegisterCors();

            //builder.Services.AddSingleton<SignalRCustomAuthorizeAttribute/**/, AuthorizeAttribute>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                //options.Cookie.SecurePolicy.HasFlag(CookieSecurePolicy.Always);
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.IdleTimeout = TimeSpan.FromSeconds(1800);
                options.Cookie.SameSite = SameSiteMode.None;
            });
            /*builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
                                              ? CookieSecurePolicy.None
                                              : CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });*/

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        //.Select(x => x.ErrorMessage)      // !!! Use only in development to preview senstiive error message 
                        /////////
                        .Select(x => MapErrorMessage(x.ErrorMessage))   // Use this in production to prevent senstiive error message exposure
                        .ToList();

                    var result = new ValidationResultModel
                    {
                        Status = false,
                        Message = "Some Errors were found ",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };

                // Method to map detailed error messages to user-friendly messages
                string MapErrorMessage(string errorMessage)
                {
                    if (errorMessage.Contains("The JSON value could not be converted"))
                        return "Invalid data format. Please check your input.";

                    else if (errorMessage.Contains("The request field is required"))
                        return "Some required fields are missing.";

                    return "An error occurred. Please check your input.";
                }

            });

            builder.Services.AddLogging(options =>
            {
                options.AddLog4Net();
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            /*builder.Services.AddSwaggerGen( x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Dite API", Version = "v1" });
            });*/

            builder.Services.AddSwaggerGen(swagger =>
            {
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \\r\\n\\r\\n Enter 'Bearer' [space] and then your token in the text input below.\\r\\n\\r\\nExample: \\\"Bearer 12345abcdef\\\"\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           /* if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Dite API v1");
                    x.RoutePrefix = string.Empty;
                });
            }*/

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Dite API v1");
                x.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy");
            app.UseSession();
            //app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<JsonExceptionMiddleware>();
            app.UseAuthorization();
            app.MapHub<MessageHub>("/api/message-hub");
            //app.MapHub<NotificationHub>("/api/notification-hub");
            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/api/discussion-hub");
            });*/
            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/api/message-hub");
            });*/
            app.MapControllers();

            app.Run();
        }
    }
}
