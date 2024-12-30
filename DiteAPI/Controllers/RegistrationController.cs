using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.infrastructure.Data.Entities;
using DiteAPI.infrastructure.Infrastructure.Persistence;
using DiteAPI.Infrastructure.Config;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RegistrationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public RegistrationController(IMediator mediator, ILogger<RegistrationController> logger, IConfiguration configuration, DataDBContext dbContext, IOptions<AppSettings> options)
        {
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
            _appSettings = options.Value;
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

        [HttpPost("user")]
        //public async Task<IActionResult> RegisterNewUser([FromForm] RegistrationCommand request)
        public async Task<IActionResult> RegisterNewUser(RegistrationCommand request)
        {
            try
            {
                var modelxfmed = new RegistrationCommand { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, UserName = request.UserName, };
                //var modelxfmed = new RegistrationCommand { FirstName = request.FirstName, LastName = request.LastName, MiddleName = request.MiddleName, DateOfBirth = request.DateOfBirth, UserGender = request.UserGender, Email = request.Email, UserName = request.UserName, PhoneNumber = request.PhoneNumber };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"REGISTRATION_CONTROLLER => User attempt to REGISTER {req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPost("uniqueEmail")]
        public async Task<IActionResult> IsEmailUnique(UniqueEmailCheckRequest request)
        {
            try
            {
                var modelxfmed = new UniqueEmailCheckRequest { Email = request.Email };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"REGISTRATION_CONTROLLER => Checking EMAIL uniqueness as User attempt to REGISTER {req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPost("uniqueUsername")]
        public async Task<IActionResult> IsUsernameUnique(UniqueUsernameCheckRequest request)
        {
            try
            {
                var modelxfmed = new UniqueUsernameCheckRequest { Username = request.Username };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"REGISTRATION_CONTROLLER => Checking USERNAME uniqueness as User attempt to REGISTER {req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"REGISTRATION_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
} 
