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
    public class TaskManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        ILogger<TaskManagerController> _logger;
        private readonly AppSettings _appSettings;

        public TaskManagerController(IMediator mediator, ILogger<TaskManagerController> logger, IOptions<AppSettings> appSettings)
        {
            _mediator = mediator;
            _logger = logger;
            _appSettings = appSettings.Value;
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
        public async Task<IActionResult> GetTaskStatusEnumValues()
        {
            try
            {
                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to GET all available Task Status Enum values");
                var response = await _mediator.Send(new GetTaskStatusEnumValuesQuery());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("all-tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to GET all available Task Status Enum values");
                var response = await _mediator.Send(new GetAllTasksQuery());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute]DeleteTaskCommand request)
        {
            try
            {
                var modelxfmed = new DeleteTaskCommand { TaskId = request.TaskId };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to DELETE an existing TASK\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpPatch("update-task")]
        public async Task<IActionResult> UpdateTask(UpdateTaskCommand request)
        {
            try
            {
                var modelxfmed = new UpdateTaskCommand { TaskId = request.TaskId, TaskTitle = request.TaskTitle, TaskDescription = request.TaskDescription, TaskDueDate = request.TaskDueDate, TaskCourseTag = request.TaskCourseTag };
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to EDIT an existing TASK\n{req}");
                var response = await _mediator.Send(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TASK_MANAGER_CONTROLLER => Something went wrong\n {ex.StackTrace}: {ex.Message}");
                return StatusCode(500, $"{_appSettings.ProcessingError}");
            }
        }

        [HttpGet("completion-rate")]
        public async Task<IActionResult> GetTaskCompletionRate([FromQuery] GetTaskCompletionRateQuery request)
        {
            try
            {
                var modelxfmed = new GetTaskCompletionRateQuery { TimeFilter = request.TimeFilter};
                var req = JsonConvert.SerializeObject(modelxfmed);

                _logger.LogInformation($"TASK_MANAGER_CONTROLLER => User attempt to GET TASK_COMPLETION_RATE\n{req}");
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
