using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Auth;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DiteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", true);
            //builder.Configuration.AddJsonFile("appsettings.json", true, true).Build();

            builder.Services.RegisterApplication();
            //builder.Services.RegisterCors();
            builder.Services.RegisterPersistence(builder.Configuration);
            builder.Services.RegisterIdentity();
            builder.Services.RegisterJwt(builder.Configuration);
            builder.Services.RegisterContactInformation(builder.Configuration);
            builder.Services.RegisterMailKitSection(builder.Configuration);
            builder.Services.RegisterEmailVerification(builder.Configuration);

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value!.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var result = new ValidationResultModel
                    {
                        Status = true,
                        Message = "Some Errors were found ",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            // Logging
            builder.Services.AddLogging(options =>
            {
                options.AddLog4Net();
            });

            // MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseCors("MyCorsPolicy");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
