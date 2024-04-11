using Appointify.Application.Queries.Events.Day;
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

        [HttpGet("day")]
        public async Task<IActionResult> GetDayEventsAsync()
        {
            var events = await _mediator.Send(new GetDayEventsQuery());
            return Ok(events);
        }
    }
}
