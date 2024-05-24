using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RegistrationController> _logger;
        private readonly IOptions<ContactInformation> _contactOptions;
        private readonly DataDBContext _dbContext;

        public RegistrationController(IMediator mediator, ILogger<RegistrationController> logger, IOptions<ContactInformation> contactOptions, DataDBContext dbContext)
        {
            _mediator = mediator;
            _logger = logger;
            _contactOptions = contactOptions;
            _dbContext = dbContext;
        }

      /*  /// <summary>
        /// Handle Hospital registration
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A baseresponse of object</returns>
        /// <response code="200">Operation successful</response>
        /// <response code="400">If validation fails due to validation errors or application encountered an exception</response>
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]*/

        [HttpPost("RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser([FromForm] Registration request)
        {
            BaseResponse<string> response;
            try
            {
                response = await _mediator.Send(request);
                if (!response.Status)
                    return BadRequest(response);
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"We encountered an issue while processing your registration request. You may try to register again, or for further assistance, please contact our Support Team at {_contactOptions.Value.EmailAddress}");
            }

            return Ok(response);
        }

        [HttpPatch("VerifyEmail")]
        public async Task<IActionResult> VerifyAccountEmail([FromBody] VerifyAccountOtpRequest request)
        {
            BaseResponse response;
            try
            {
                response = await _mediator.Send(request);
                if (!response.Status)
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"We encountered an issue while processing your registration request. You may try to register again, or for further assistance, please contact our Support Team at {_contactOptions.Value.EmailAddress}");
            }

            return Ok(response);
        }
    }
}
