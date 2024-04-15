using Appointify.Domain;
using MediatR;
using System.Text.Json.Serialization;

namespace Appointify.Application.Commands.Services.Update
{
    public class UpdateServiceCommand : IRequest<Nothing>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Interval { get; set; }

        public UpdateServiceCommand WithId(Guid id)
        {
            Id = id;
            return this;
        }
    }
}
