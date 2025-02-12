using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VerificationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public VerificationController(IMediator mediator, ILogger<VerificationController> logger, IConfiguration configuration, DataDBContext dbContext, IOptions<AppSettings> options)
        {
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
            _appSettings = options.Value;
        }

        [HttpPost("verify-otp")]
        //public async Tasks<IActionResult> VerifyOtp([FromForm] VerifyOtpCommand request)
        public async Task<IActionResult> VerifyOtp(VerifyOtpCommand request)
        {
            try
            {
                var modelxfmed = new VerifyOtpCommand { Code = request.Code, Purpose = request.Purpose };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"VERIFICATION_CONTROLLER => User attempt to Verify Otp \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"VERIFICATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPost("send-otp")]
        //public async Tasks<IActionResult> SendOtpCode([FromBody] SendOtpCommand request)
        public async Task<IActionResult> SendOtpCode(SendOtpCommand request)
        {
            try
            {
                var modelxfmed = new SendOtpCommand { Recipient = request.Recipient, RecipientType = request.RecipientType, Purpose = request.Purpose };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"VERIFICATION_CONTOLLER => User attempt to SendOtpCode \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"VERIFICATION_CONTOLLER  => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
