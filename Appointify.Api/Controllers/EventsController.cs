﻿using Appointify.Application.Commands.Events.Create;
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

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
