using Azure;
using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISessionService _sessionService;
        ILogger<AuthController> _logger;
        private readonly AppSettings _appSettings;
        public AuthController(IMediator mediator, ILogger<AuthController> logger, ISessionService sessionService, IOptions<AppSettings> options)
        {
            _mediator = mediator;
            _logger = logger;
            _sessionService = sessionService;
            _appSettings = options.Value;
        }

        /// <summary>
        /// Handles Sign-in of users
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A BaseResponse of object</returns>
        /// <response code="200"> Operation successful</response>
        /// <response code="400">If validation fails due to validation errors"</response>
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AuthRequest request)
        {
            try
            {
                var modelxfmed = new AuthRequest { Email = request.Email };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"AUTH_CONTROLLER => User attempt to LOGIN \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand request)
        { 
            try
            {
                _logger.LogInformation($"AUTH_CONTROLLER => User attempt to reset password \nUserId: {_sessionService.GetStringFromSession("UserId")}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError($"AUTH_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}