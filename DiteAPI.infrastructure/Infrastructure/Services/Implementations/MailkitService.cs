using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class MailkitService : IMailkitService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailkitService>_logger;

        public MailkitService(IConfiguration configuration, ILogger<MailkitService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<BaseResponse> SendEmailAsync(SingleEmailRequest request)
        {
           try
           {
               _logger?.LogInformation($"MAILKIT_SEND_MAIL_SERVICE => Sending email attempt with mailkit | Email - {request.RecipientEmailAddress}");

               var email = new MimeMessage();
               email.From.Add(new MailboxAddress(_configuration["MailKitSection:DisplayName"], _configuration["MailKitSection:EmailUsername"]));
               email.To.Add(MailboxAddress.Parse(request.RecipientEmailAddress));
               email.Subject = request.EmailSubject;
               var bodyBuilder = new BodyBuilder();

               bodyBuilder.HtmlBody = request.HtmlEmailBody;
               bodyBuilder.TextBody = request.PlainEmailBody;
               email.Body = bodyBuilder.ToMessageBody();
               email.ReplyTo.Add(MailboxAddress.Parse("support@dite.com"));

               using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["MailKitSection:EmailHost"], _configuration.GetValue<int>("MailKitSection:Port"), SecureSocketOptions.SslOnConnect);
                //await smtp.ConnectAsync(_configuration["MailKitSection:EmailHost"], _configuration.GetValue<int>("MailKitSection:Port"), _configuration.GetValue<SecureSocketOptions>("MailKitSection:SocketOption"));
               smtp.Authenticate(_configuration["MailKitSection:EmailUsername"], _configuration["MailKitSection:EmailPassword"]);
               await smtp.SendAsync(email).ConfigureAwait(false);
               await smtp.DisconnectAsync(true);

               return new BaseResponse(true, "Email sent successfully");
           }
           catch (Exception ex)
           {
               _logger?.LogError($"MAILKIT_SEND_MAIL_SERVICE => Application ran into an error while trying to send email\n {ex.StackTrace}: {ex.Message}");
               return new BaseResponse(false, "Application ran into an error while trying to send email");
           }
        }

        public Task<BaseResponse> SendEmailToMultipleRecipientsAsync(MultipleEmailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
