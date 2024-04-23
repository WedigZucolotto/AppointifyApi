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
    }
}
