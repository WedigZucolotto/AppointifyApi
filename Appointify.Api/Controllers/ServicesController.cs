using Appointify.Application.Commands.Services.Create;
using Appointify.Application.Commands.Services.Delete;
using Appointify.Application.Commands.Services.Update;
using Appointify.Application.Queries.Services.GetAll;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllServicesQuery query)
        {
            var services = await _mediator.Send(query);
            return Ok(services);
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateServiceCommand command)
        {
            await _mediator.Send(command.WithId(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteServiceCommand(id));
            return NoContent();
        }
    }
}
