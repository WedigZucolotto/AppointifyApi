using Appointify.Application.Commands.Companies.Update;
using Appointify.Application.Queries.Companies.AvailableTimes;
using Appointify.Application.Queries.Companies.ById;
using Appointify.Application.Queries.Companies.ToSchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Appointify.Infrastructure.Authentication;

namespace Appointify.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : Controller
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.Companies.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var company = await _mediator.Send(new GetCompanyByIdQuery(id));
            return Ok(company);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Companies.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCompanyCommand command)
        {
            await _mediator.Send(command.WithId(id));
            return NoContent();
        }

        [HttpGet("{id}/available-times")]
        public async Task<IActionResult> GetAvailableTimesAsync([FromRoute] Guid id, [FromQuery] GetAvailableTimesQuery query)
        {
            var times = await _mediator.Send(query.WithId(id));
            return Ok(times);
        }

        [HttpGet("{id}/to-schedule")]
        public async Task<IActionResult> GetScheduleAsync([FromRoute] Guid id)
        {
            var company = await _mediator.Send(new GetCompanyToScheduleQuery(id));
            return Ok(company);
        }
    }
}
