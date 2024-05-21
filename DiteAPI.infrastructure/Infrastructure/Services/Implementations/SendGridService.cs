using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructure.Utilities.DataExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SendGridErrorResponse = DiteAPI.infrastructure.Data.Models.SendGridErrorResponse;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class SendGridService : ISendGridService
    {
        private readonly IConfiguration _configuration;
        private readonly DataDBContext _dbContext;
        private readonly ILogger<SendGridService> _logger;

        public SendGridService(IConfiguration configuration, DataDBContext dbContext, ILogger<SendGridService> logger)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<BaseResponse> SendEmailAsync(SingleEmailRequest request)
        {
            _logger.LogInformation($"SENDGRID_SEND_SINGLE_EMAIL => Sending email attempt with SendGrid | Email - {request.RecipientEmailAddress}");

            try
            {
                var client = new SendGridClient(_configuration["SendGrid:APIKey"]);
                var from = new EmailAddress(_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]);
                string subject = request.EmailSubject ?? "";
                var to = new EmailAddress(request.RecipientEmailAddress, request.RecipientName);
                var plainTextContent = request.PlainEmailBody;
                var htmlContent = request.HtmlEmailBody;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

                if (response?.StatusCode == HttpStatusCode.Accepted) return new BaseResponse(true, "Email Sent Successfully");
                string errorMessage = "";
                if (response != null)
                {
                    string responseMessage = await response.Body.ReadAsStringAsync().ConfigureAwait(false);
                    var responseObject = JsonConvert.DeserializeObject<SendGridErrorResponse>(responseMessage);
                    var errors = responseObject?.Errors?.Select(e => e.Message).ToList();
                    errorMessage = string.Join(" ", errors ?? new List<string?>());
                }

                _logger?.LogInformation($"SENDGRID_SEND_SINGLE_EMAIL => The email sending attempt failed. | Reason - {errorMessage}");
                return new BaseResponse(false, errorMessage);
            }
            catch(Exception ex)
            {
                _logger.LogError($"SENDGRID_SEND_EMAIL => Application ran into an error while trying to send email\n {ex.StackTrace}: {ex.Message}");

                return new BaseResponse(false, "The email sending attempt failed");
            }
        }

        public async Task<BaseResponse> SendEmailToMultipleRecipientsAsync(MultipleEmailRequest request)
        {
            _logger?.LogInformation($"SENDGRID_SEND_MULTIPLE_EMAIL => Sending email attempt with sendgrid | Email - {string.Join(",", request.RecipientEmailAddress!)}");
            try
            {
                string errorMessage = "";

                if (request.RecipientEmailAddress.IsAny())
                {
                    var emailAddresses = new List<EmailAddress>();
                    foreach(var emailAddress in request.RecipientEmailAddress!)
                    {
                        emailAddresses.Add(new EmailAddress { Email = emailAddress, Name = emailAddress });
                    }

                    var client = new SendGridClient(_configuration["SendGrid:APIKey"]);
                    var from = new EmailAddress(_configuration["SendGrid:FromEmail"], _configuration["SendGrid:FromName"]);
                    string subject = request.EmailSubject ?? "";
                    var to = emailAddresses;
                    var plainTextContent = request.PlainEmailBody;
                    var htmlContent = request.HtmlEmailBody;
                    var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);

                    var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

                    if (response?.StatusCode == HttpStatusCode.Accepted) return new BaseResponse(true, "Email sent successfully");
                    if (response != null)
                    {
                        string responseMessage = await response.Body.ReadAsStringAsync().ConfigureAwait(false);
                        var responseObject = JsonConvert.DeserializeObject<SendGridErrorResponse>(responseMessage);
                        var errors = responseObject?.Errors?.Select(x => x.Message).ToList();
                        errorMessage = string.Join(" ", errors ?? new List<string?>());
                    }
                }

                _logger?.LogInformation($"SENDGRID_SEND_MULTIPLE_EMAIL => Sending email attempt was not successful. | Reason - {errorMessage}");
                return new BaseResponse(false, errorMessage);
            }
            catch(Exception ex )
            {
                _logger?.LogError($"SENDGRID_SEND_EMAIL => Application ran into an error while trying to send email\n {ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, "The email sending attempt failed");
            }
        }

        public async Task<BaseResponse> SendEmailWithSMTP(SingleEmailRequest request)
        {
            _logger?.LogInformation($"SMTP_SEND_MAIL_SERVICE => Sending email attempt with sendgrid | Email - {request.RecipientEmailAddress}");

            try
            {
                MailMessage mail = new MailMessage
                {
                    Subject = request.EmailSubject,
                    Body = request.HtmlEmailBody,
                    From = new MailAddress(_configuration["SMTPConfig:Sender"]!, _configuration["SMTPConfig:DisplayName"]),
                    To = { request.RecipientEmailAddress! },
                    IsBodyHtml = bool.Parse(_configuration["SMTPConfig:IsBodyHtml"]!)
                };

                NetworkCredential networkCredential = new NetworkCredential(_configuration["SMTPConfig:Username"], _configuration["SMTPConfig:Password"]);

                SmtpClient smtpClient = new SmtpClient
                {
                    Host = _configuration["SMTPConfig:Host"]!,
                    Port = int.Parse(_configuration["SMtPConfig:Port"]!),
                    EnableSsl = bool.Parse(_configuration["SMTPConfig:EnableSSL"]!),
                    UseDefaultCredentials = bool.Parse(_configuration["SMTPConfig:UseDefaultCredentials"]!),
                    Credentials = networkCredential
                };

                mail.BodyEncoding = Encoding.Default;
                await smtpClient.SendMailAsync(mail).ConfigureAwait(false);

                return new BaseResponse(true, "Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError($"SMTP_SEND_MAIL_SERVICE => Application ran into an error while trying to send email\n {ex.StackTrace}: {ex.Message}");
                return new BaseResponse(false, "Application ran into an error while trying to send email");
            }

        }
    }
}
