using Appointify.Application.Commands.Companies.Create;
using Appointify.Application.Commands.Companies.Delete;
using Appointify.Application.Commands.Companies.Update;
using Appointify.Application.Queries.Companies.All;
using Appointify.Application.Queries.Companies.AvailableTimes;
using Appointify.Application.Queries.Companies.ById;
using Appointify.Application.Queries.Companies.ToSchedule;
using Appointify.Application.Queries.Companies.Ids;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var companies = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var company = await _mediator.Send(new GetCompanyByIdQuery(id));
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateCompanyCommand command)
        {
            await _mediator.Send(command.WithId(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteCompanyCommand(id));
            return NoContent();
        }

        [HttpGet("ids")]
        public async Task<IActionResult> GetIdsAsync()
        {
            var ids = await _mediator.Send(new GetAllCompaniesIdsQuery());
            return Ok(ids);
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
