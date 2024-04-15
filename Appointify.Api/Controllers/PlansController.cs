using Appointify.Application.Queries.Plans.Options;
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
        public async Task<IActionResult> GetOptionsAsync()
        {
            var plans = await _mediator.Send(new GetPlanOptionsQuery());
            return Ok(plans);
        }
    }
}
