using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<MessagesController> _logger;
        private readonly AppSettings _appSettings;

        public MessagesController(IMediator mediator, ILogger<MessagesController> logger, IOptions<AppSettings> options)
        {
            _mediator = mediator;
            _logger = logger;
            _appSettings = options.Value;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostMessage(PostMessageCommand request)
        {
            try
            {
                //var modelxfmed = new PostMessageCommand { MessageTitle = request.MessageTitle, MessageBody = request.MessageBody, TrackId = request.TrackId, SenderId = request.SenderId };
                var modelxfmed = new PostMessageCommand { MessageTitle = request.MessageTitle, MessageBody = request.MessageBody, TrackId = request.TrackId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"MESSAGE_CONTROLLER => User attempt to POST a MESSAGE \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MESSAGE_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessageDetails([FromRoute] GetMessageDetailsQuery request)
        {
            try
            {
                var modelxfmed = new GetMessageDetailsQuery { MessageId = request.MessageId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"MESSAGE_CONTROLLER => User attempt to GET message Details \n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MESSAGE_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPost("responses")]
        public async Task<IActionResult> PostReplyToMessage(PostReplyCommand request)
        {
            try
            {
                var modelxfmed = new PostReplyCommand { ParentId = request.ParentId, ResponseMessage = request.ResponseMessage };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"MESSAGE_CONTROLLER => User attempt to POST a reply to message\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MESSAGE_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
