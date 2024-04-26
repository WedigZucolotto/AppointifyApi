using Appointify.Domain;
using MediatR;
using System.Text.Json.Serialization;

namespace Appointify.Application.Commands.Users.Update
{
    public class UpdateUserCommand : IRequest<Nothing>
    {
        public UpdateUserCommand WithId(Guid id)
        {
            Id = id;
            return this;
        }

        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Password { get; set; } = string.Empty;

        public string? CompleteName { get; set; } = string.Empty;
    }
}
