using Appointify.Application.Commands.Events.Create;
using Appointify.Application.Commands.Events.Delete;
using Appointify.Application.Queries.Events.ById;
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
        [HasPermission(Permissions.Events.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetEventByIdQuery query)
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

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Events.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteEventCommand(id));
            return NoContent();
        }
    }
}
