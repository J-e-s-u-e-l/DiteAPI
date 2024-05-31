using Azure;
using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Data.Models;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<AuthController> _logger;
        IConfiguration _configuration;
        public AuthController(IMediator mediator, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
        } 

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] AuthRequest request)
        {
            BaseResponse<LoginResponse> response;
            try
            {
                response = await _mediator.Send(request);
                if (!response.Status)
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"We encountered an issue while processing your registration request. You may try to register again, or for further assistance, please contact our Support Team at {_configuration["ContactInformation:EmailAddress"]}");
            }

            return Ok(response);
        }


        /*[HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand request)
        {

        }*/
    }
}