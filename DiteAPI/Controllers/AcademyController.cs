using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Auth;
using DiteAPI.Infrastructure.Infrastructure.Services.Implementations;
using DiteAPI.Infrastructure.Infrastructure.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class AcademyController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<AuthController> _logger;
        private readonly AppSettings _appSettings;

        public AcademyController(IMediator mediator, ILogger<AuthController> logger, IOptions<AppSettings> options)
        {
            _mediator = mediator;
            _logger = logger;
            _appSettings = options.Value;
        }
        /// <summary>
        /// Handles creation of Academy
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A BaseResponse of object</returns>
        /// <response code="200"> Operation successful</response>
        /// <response code="400">If validation fails due to validation errors"</response>
        /// <response code="500">If application encountered an exception"</response>
        [HttpPost("create-academy")]
        public async Task<IActionResult> CreateAcademy([FromForm] CreateAcademyCommand request)
        {
            try
            {
                var modelxfmed = new CreateAcademyCommand { AcademyName = request.AcademyName, Tracks = request.Tracks};
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to CREATE new ACADEMY {req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPost("join-academy")]
        public async Task<IActionResult> JoinAcademy([FromForm] JoinAcademyCommand request)
        {
            try
            {
                var modelxfmed = new JoinAcademyCommand { AcademyCode = request.AcademyCode };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to JOIN an ACADEMY {req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
