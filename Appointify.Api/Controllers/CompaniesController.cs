using Appointify.Application.Queries.Companies;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyByIdAsync([FromRoute] Guid id)
        {
            var company = await _mediator.Send(new GetCompanyByIdQuery(id));
            return Ok(company);
        }
    }
}
