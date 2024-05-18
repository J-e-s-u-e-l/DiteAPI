using Azure;
using DiteAPI.Api.Application.CQRS.Queries;
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
        //private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        } 

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] AuthRequest request)
        {
            var response = await _mediator.Send(request);
            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
