using Appointify.Application.Commands.Services.Create;
using Appointify.Application.Queries.Services.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointify.Api.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : Controller
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var services = await _mediator.Send(new GetServiceOptionsQuery());
            return Ok(services);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateServiceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
