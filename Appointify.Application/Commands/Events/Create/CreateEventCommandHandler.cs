using Appointify.Domain;
using Appointify.Domain.Entities;
using Appointify.Domain.Notifications;
using MediatR;
using System.Net.Http;

namespace Appointify.Application.Commands.Events.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Nothing>
    {
        //private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;

        public CreateEventCommandHandler(INotificationContext notification) 
        {
            _notification = notification;
        }

        public async Task<Nothing> Handle(CreateEventCommand command, CancellationToken cancellationToken) 
        {
            // se UserId vier do token -> Funcionário criou evento
            // se UserId vier da request -> Cliente escolheu Funcionário
            // se UserId não vier -> Escolher usuário aleátório

            //var userId = await _httpContext.GetUserIdAsync() ?? command.UserId;

            //var user = await _userRepository.GetById(userId);

            //if (user is null)
            //{
            //    _notification.AddNotFound("User does not exists");
            //    return default;
            //}


            //var description = $"Marcado por: {command.Name} às {command.CreatedAt}\nContato: {command.Contact}";


            //var newEvent = new Event(command.Name, description);


            return Nothing.Value;
        }
    }
}
