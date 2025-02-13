using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class AcademiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<AcademiesController> _logger;
        private readonly AppSettings _appSettings;

        public AcademiesController(IMediator mediator, ILogger<AcademiesController> logger, IOptions<AppSettings> options)
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
        public async Task<IActionResult> CreateAcademy(CreateAcademyCommand request)
        {
            try
            {
                var modelxfmed = new CreateAcademyCommand { AcademyName = request.AcademyName, Tracks = request.Tracks};
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to CREATE new ACADEMY \n{req}");
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
        public async Task<IActionResult> JoinAcademy(JoinAcademyCommand request)
        {
            try
            {
                var modelxfmed = new JoinAcademyCommand { AcademyCode = request.AcademyCode };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to JOIN an ACADEMY \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("user-academies")]
        public async Task<IActionResult> GetUserAcademies()
        {
            try
            {
                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET all joined ACADEMY");
                var response = await _mediator.Send(new GetUserAcademiesQuery());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
        
        [HttpGet("{academyId}")]
        public async Task<IActionResult> GetAcademyDetails([FromRoute] Guid academyId)
        {
            try
            {
                var modelxfmed = academyId;
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET ACADEMY Details \n{req}");
                var response = await _mediator.Send(new GetAcademyDetailsQuery(academyId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
        
        [HttpDelete("{academyId}/leave")]
        public async Task<IActionResult> LeaveAcademy([FromRoute] Guid academyId)
        {
            try
            {
                var modelxfmed = academyId;
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to LEAVE ACADEMY \n{req}");
                var response = await _mediator.Send(new LeaveAcademyCommand(academyId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("{academyId}/members")]
        public async Task<IActionResult> GetAllMembers([FromRoute] Guid academyId)
        {
            try
            {
                var modelxfmed = academyId;
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET all MEMBERS in the ACADEMY \n{req}");
                var response = await _mediator.Send(new GetAllMembersQuery(academyId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("{academyId}/tracks")]
        public async Task<IActionResult> GetAllTracks([FromRoute] Guid academyId)
        {
            try
            {
                var modelxfmed = academyId;
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET all TRACKS in the ACADEMY \n{req}");
                var response = await _mediator.Send(new GetAllTracksQuery(academyId));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPut("members/change-role")]
        public async Task<IActionResult> ChangeAcademyMemberRole(ChangeAcademyMemberRoleCommand request)
        { 
            try
            {
                var modelxfmed = new ChangeAcademyMemberRoleCommand { MemberId = request.MemberId, NewRole = request.NewRole, AssignedTracksIds = request.AssignedTracksIds, AcademyId = request.AcademyId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to CHANGE ACADEMY MEMBER ROLE \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPut("members/remove-member")]
        public async Task<IActionResult> RemoveMemberFromAcademy(RemoveMemberFromAcademyCommand request)
        { 
            try
            {
                var modelxfmed = new RemoveMemberFromAcademyCommand { MemberId = request.MemberId, AcademyId = request.AcademyId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to REMOVE MEMBER from ACADEMY\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetAllMessagesInAcademy([FromQuery] GetAllMessagesQuery request)
        { 
            try
            {
                var modelxfmed = new GetAllMessagesQuery { AcademyId = request.AcademyId, PageNumber = request.PageNumber, PageSize = request.PageSize };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET all MESSAGES in ACADEMY\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("{academyId}/academy-info")]
        public async Task<IActionResult> GetAcademyInfo([FromRoute] GetAcademyInfoQuery request)
        { 
            try
            {
                var modelxfmed = new GetAcademyInfoQuery{ AcademyId = request.AcademyId};
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET Academy info\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPost("uploadResource")]
        public async Task<IActionResult> UploadResource([FromForm]UploadResourceCommand request)
        { 
            try
            {
                var modelxfmed = new UploadResourceCommand { AcademyId = request.AcademyId, ResourceName = request.ResourceName, ResourceType = request.ResourceType, File = request.File};
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to UPLOAD a resource to the Academy resource repo\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("{academyId}/get-all-resources")]
        public async Task<IActionResult> GetAllResourcesInAcademyRepo([FromRoute] GetAllResourcesInAcademyRepoQuery request)
        { 
            try
            {
                var modelxfmed = new GetAllResourcesInAcademyRepoQuery { AcademyId = request.AcademyId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to GET all available resources in the Academy resource repo\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpDelete("{resourceId}")]
        public async Task<IActionResult> DeleteResourceInAcademyRepo([FromRoute] DeleteResourceInAcademyRepoCommand request)
        { 
            try
            {
                var modelxfmed = new DeleteResourceInAcademyRepoCommand { ResourceId = request.ResourceId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to DELETE resource from the Academy resource repo\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("{resourceId}/download")]
        public async Task<IActionResult> DownloadResourceInAcademyRepo([FromRoute] DownloadResourceInAcademyRepoCommand request)
        { 
            try
            {   
                var modelxfmed = new DeleteResourceInAcademyRepoCommand { ResourceId = request.ResourceId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"ACADEMY_CONTROLLER => User attempt to DOWNLOAD resource from the Academy resource repo\n{req}");
                var response = await _mediator.Send(request);

                //return File(response.Data.Resource, response.Data.ContentType, response.Data.FileName);
                return new FileStreamResult(response.Data.Resource, response.Data.ContentType)
                {
                    FileDownloadName = response.Data.FileName
                };

                //return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ACADEMY_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
