using Appointify.Application.Commands.Services.Create;
using Appointify.Application.Commands.Services.Delete;
using Appointify.Application.Commands.Services.Update;
using Appointify.Application.Queries.Services.All;
using Appointify.Application.Queries.Services.ById;
using Appointify.Application.Queries.Services.Options;
using Appointify.Infrastructure.Authentication;
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

        [HttpGet]
        [HasPermission(Permissions.Services.GetAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllServicesQuery query)
        {
            var services = await _mediator.Send(query);
            return Ok(services);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.Services.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var service = await _mediator.Send(new GetServiceByIdQuery(id));
            return Ok(service);
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetOptionsAsync([FromQuery] GetServiceOptionsQuery query)
        {
            var options = await _mediator.Send(query);
            return Ok(options);
        }

        [HttpPost]
        [HasPermission(Permissions.Services.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateServiceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Services.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateServiceCommand command)
        {
            await _mediator.Send(command.WithId(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Services.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteServiceCommand(id));
            return NoContent();
        }
    }
}
