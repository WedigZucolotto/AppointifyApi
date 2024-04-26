using Appointify.Application.Commands.Users.Create;
using Appointify.Application.Commands.Users.Delete;
using Appointify.Application.Commands.Users.Login;
using Appointify.Application.Commands.Users.Update;
using Appointify.Application.Queries.Users.All;
using Appointify.Application.Queries.Users.ById;
using Appointify.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointify.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HasPermission(Permissions.Users.GetAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllUsersQuery query)
        {
            var users = await _mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.Users.GetById)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost()]
        [HasPermission(Permissions.Users.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Users.Update)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command.WithId(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Users.Delete)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}
