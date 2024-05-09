using Appointify.Application.Commands.Plans.Create;
using Appointify.Application.Commands.Plans.Delete;
using Appointify.Application.Commands.Plans.Update;
using Appointify.Application.Queries.Plans.All;
using Appointify.Application.Queries.Plans.ById;
using Appointify.Application.Queries.Plans.Options;
using Appointify.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointify.Api.Controllers
{
    [ApiController]
    [Route("api/plans")]
    public class PlansController : Controller
    {
        private readonly IMediator _mediator;

        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("options")]
        [HasPermission(Permissions.Plans.Options)]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var plans = await _mediator.Send(new GetPlanOptionsQuery());
            return Ok(plans);
        }

        [HttpGet]
        [HasPermission(Permissions.Plans.GetAll)]
        public async Task<IActionResult> GetAllAsync()
        {
            var plans = await _mediator.Send(new GetAllPlansQuery());
            return Ok(plans);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.Plans.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var plan = await _mediator.Send(new GetPlanByIdQuery(id));
            return Ok(plan);
        }

        [HttpPost]
        [HasPermission(Permissions.Plans.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePlanCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Plans.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdatePlanCommand command)
        {
            await _mediator.Send(command.WithId(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Plans.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeletePlanCommand(id));
            return NoContent();
        }
    }
}
