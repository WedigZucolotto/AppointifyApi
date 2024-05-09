using Appointify.Application.Commands.Events.Create;
using Appointify.Application.Queries.Companies.All;
using Appointify.Application.Queries.Events.All;
using Appointify.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointify.Api.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HasPermission(Permissions.Events.GetAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllEventsQuery query)
        {
            var events = await _mediator.Send(query);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEventCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
