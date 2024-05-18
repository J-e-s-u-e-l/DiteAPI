using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Auth;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*var env = builder.Environment;
            var parentPath = Directory.GetParent(env.ContentRootPath)?.FullName ?? "";
            builder.Configuration
                .AddJsonFile(Path.Combine(parentPath, ""))*/


            builder.Services.RegisterPersistence(builder.Configuration);
            builder.Services.RegisterIdentity();
            builder.Services.RegisterJwt(builder.Configuration);
            //DataContext
            /*builder.Services.AddDbContext<DataDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
*/
            

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
                        Status = false,
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
