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
    public class NotificationsController : ControllerBase
    {

        private readonly IMediator _mediator;
        ILogger<NotificationsController> _logger;
        private readonly AppSettings _appSettings;

        public NotificationsController(IMediator mediator, ILogger<NotificationsController> logger, IOptions<AppSettings> options)
        {
            _mediator = mediator;
            _logger = logger;
            _appSettings = options.Value;
        }

        [HttpGet("get-all-notifications")]
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                _logger.LogInformation($"NOTIFICATIONS_CONTOLLER => User attempt to GET all available NOTIFICATIONS");
                var response = await _mediator.Send(new GetAllNotificationsQuery());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"NOTIFICATIONS_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> MarkNotificationAsRead(MarkNotificationAsReadCommand request)
        {
            try
            {
                _logger.LogInformation($"NOTIFICATIONS_CONTOLLER => User attempt to MARK NOTIFICATION AS READ");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"NOTIFICATIONS_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNotification(DeleteNotificationCommand request)
        {
            try
            {
                _logger.LogInformation($"NOTIFICATIONS_CONTOLLER => User attempt to DELETE NOTIFICATION");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"NOTIFICATIONS_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("unreadNotifications-count")]
        public async Task<IActionResult> GetUnreadNotificationsCount()
        {
            try
            {
                _logger.LogInformation($"NOTIFICATIONS_CONTOLLER => User attempt to GET UNREAD NOTIFICATIONS count");
                var response = await _mediator.Send(new GetUnreadNotificationsCountQuery());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"NOTIFICATIONS_CONTOLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
