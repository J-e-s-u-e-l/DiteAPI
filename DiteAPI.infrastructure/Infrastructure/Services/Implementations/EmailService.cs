using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using DiteAPI.infrastructure.Infrastructures.Utilities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMailkitService _mailkitService;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            ILogger<EmailService> logger,
            IMailkitService mailkitService,
            IWebHostEnvironment webHostEnvironment
            //IHttpClientFactory httpClientFactory
            )
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            //_httpClientFactory = httpClientFactory;
            _mailkitService = mailkitService;
        }

     /*   public async Task<BaseResponse> CheckDisposableEmailAsync(string email)
        {
            _logger.LogInformation($"EMAIL_SERVICE => Checkinig disposable email | Email - {email}");

            if (_webHostEnvironment.EnvironmentName == "Development")
            {
                _logger.LogInformation($"EMAIL_SERVICE => check cancelled. Environment is development | Email - {email}");
                return new BaseResponse(true, "Operation finished. Environment is development.");
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://verifier.meetchopra.com/verify/{email}?token=2b1e810090b21cab8a8753ec6bd1f091ebd1d9dd6b6ddbfea9e9b45c1b55757fba354cbe95a14fcad448107ae138e450");

                var client = _httpClientFactory.CreateClient();
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    _logger.LogInformation($"EMAIL_SERVICE => Checking disposable email completed | Result - {responseString}");
                    var responseResult = JsonConvert.DeserializeObject<MeetchopraValidResponse>(responseString);
                    return new BaseResponse(responseResult?.status ?? false, "Operation completed.");
                }
                else
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    _logger.LogInformation($"EMAIL_SERVICE => Checking disposable email completed with error | Result - {responseString}");
                    return new BaseResponse(true, "Operation finished with exception.");
                }
            }
            catch ( Exception ex )
            {
                _logger.LogError($"EMAIL_SERVICE => Checking disposable email failed with exception \n{ex.StackTrace}: {ex.Message}");
                return new BaseResponse(true, "Operation finished with exception");
            }
        }*/

        public async Task<BaseResponse<EmailBodyResponse>> GetEmailBody(EmailBodyRequest request)
        {
            string emailFolderName = "emails"; string plainTextFolderName = "plaintexts";
            var response = new EmailBodyResponse { HtmlBody = "", PlainBody = "" };
            string fileName = "", plainFileName = "";

            string? templateFile = null; string? plainTextFile = null;

            switch (request.EmailTitle)
            {
                case EmailTitleEnum.EMAILVERIFICATION:
                    fileName = "emailVerification.html";
                    templateFile = await GetTemplateFileAsync(emailFolderName, fileName);
                    if (!string.IsNullOrWhiteSpace(templateFile))
                        response.HtmlBody = templateFile;

                    plainFileName = "emailVerification.txt";
                    plainTextFile = await GetTemplateFileAsync(plainTextFolderName, plainFileName);
                    if (!string.IsNullOrWhiteSpace(plainTextFile))
                        response.PlainBody = plainTextFile;
                break;
                
                case EmailTitleEnum.PASSWORDRESET:
                    fileName = "passwordReset.html";
                    templateFile = await GetTemplateFileAsync(emailFolderName, fileName);
                    if (!string.IsNullOrWhiteSpace(templateFile))
                        response.HtmlBody = templateFile;

                    plainFileName = "passwordReset.txt";
                    plainTextFile = await GetTemplateFileAsync(plainTextFolderName, plainFileName);
                    if (!string.IsNullOrWhiteSpace(plainTextFile))
                        response.PlainBody = plainTextFile;

                break;
                
                case EmailTitleEnum.WELCOME:
                    fileName = "welcomeUser.html";
                    templateFile = await GetTemplateFileAsync(emailFolderName, fileName);
                    if (!string.IsNullOrWhiteSpace(templateFile))
                        response.HtmlBody = templateFile;

                    plainFileName = "welcomeUser.txt";
                    plainTextFile = await GetTemplateFileAsync(plainTextFolderName, plainFileName);
                    if (!string.IsNullOrWhiteSpace(plainTextFile))
                        response.PlainBody = plainTextFile;

                break;
            }

            if (string.IsNullOrEmpty(response.HtmlBody) || string.IsNullOrEmpty(response.PlainBody))
                return new BaseResponse<EmailBodyResponse>(false, "Could not fetch email body, application ran into an error while fetching email body.");

            return new BaseResponse<EmailBodyResponse>(true, "Email body fetched successfully,", response);
        }

        public async Task<string> GetTemplateFileAsync(string folderName, string fileName)
        {
            string templateFile = string.Empty;
            try
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "Assets", folderName);
                string filePath = Path.Combine(path, fileName);
                templateFile = await File.ReadAllTextAsync(filePath);
            }
            catch(Exception ex)
            {
                _logger.LogError($"EMAIL_SERVICE => Application ran into an error while fetching email template file \n{ex.StackTrace}: {ex.Message}");
            }

            if (templateFile != null)
            {
                _logger.LogInformation($"EMAIL_SERVICE => Email template fetched");
            }

            return templateFile!;
        }

        public async Task<BaseResponse> SendEmailAsync(SingleEmailRequest request)
        {
            _logger.LogInformation($"EMAIL_SERVICE => Sending email to {request.RecipientEmailAddress}");
            var response = await _mailkitService.SendEmailAsync(new SingleEmailRequest
            {
                RecipientEmailAddress = request.RecipientEmailAddress,
                RecipientName = request.RecipientName,
                EmailSubject = request.EmailSubject,
                HtmlEmailBody = request.HtmlEmailBody,
                PlainEmailBody = request.PlainEmailBody
            });

            return new BaseResponse(response.Status, response.Message!);
        }

        public async Task<BaseResponse> SendMultipleEmailAsync(MultipleEmailRequest request)
        {
            _logger?.LogInformation($"EMAIL_SERVICE => Sending email to {string.Join(",", request.RecipientEmailAddress!)}");
            var response = await _mailkitService.SendEmailToMultipleRecipientsAsync(new MultipleEmailRequest
            {
                RecipientEmailAddress = request.RecipientEmailAddress,
                RecipientName = request.RecipientName,
                EmailSubject = request.EmailSubject,
                HtmlEmailBody = request.HtmlEmailBody,
                PlainEmailBody= request.PlainEmailBody
            });

            return new BaseResponse(response.Status, response.Message!);
        }
    }
}
