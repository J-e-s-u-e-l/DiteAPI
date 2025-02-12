using DiteAPI.Api.Application.CQRS.Commands;
using DiteAPI.Api.Application.CQRS.Queries;
using DiteAPI.Infrastructure.Config;
using DiteAPI.Infrastructure.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DiteAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class TaskManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<TaskManagerController> _logger;
        private readonly AppSettings _appSettings;

        public TaskManagerController(IMediator mediator, ILogger<TaskManagerController> logger, AppSettings appSettings)
        {
            _mediator = mediator;
            _logger = logger;
            _appSettings = appSettings;
        }


        [HttpPost("new-task")]
        public async Task<IActionResult> AddNewTask(AddNewTaskCommand request)
        {
            try
            {
                var modelxfmed = new AddNewTaskCommand { TaskTitle = request.TaskTitle, TaskDescription = request.TaskDescription, TaskDueDate = request.TaskDueDate, TaskCourseTag = request.TaskCourseTag };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to ADD a new TASK\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("tasks-statuses")]
        public async Task<IActionResult> GetTaskStatusEnumValues(GetTaskStatusEnumValuesQuery request)
        {
            try
            {
                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to GET all available Task Status Enum values");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("all-tasks")]
        public async Task<IActionResult> GetAllTasks(GetAllTasksQuery request)
        {
            try
            {
                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to GET all available Task Status Enum values");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }
    }
}
