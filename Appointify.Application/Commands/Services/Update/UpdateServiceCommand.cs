using Appointify.Domain;
using MediatR;
using System.Text.Json.Serialization;

namespace Appointify.Application.Commands.Services.Update
{
    public class UpdateServiceCommand : IRequest<Nothing>
    {
        public UpdateServiceCommand WithId(Guid id)
        {
            Id = id;
            return this;
        }

        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Interval { get; set; }
    }
}
