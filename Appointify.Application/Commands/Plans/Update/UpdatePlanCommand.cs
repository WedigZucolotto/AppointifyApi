using Appointify.Domain;
using MediatR;
using System.Text.Json.Serialization;

namespace Appointify.Application.Commands.Plans.Update
{
    public class UpdatePlanCommand : IRequest<Nothing>
    {
        public UpdatePlanCommand WithId(Guid id)
        {
            Id = id;
            return this;
        }

        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public bool? ShowExtraFields { get; set; }
    }
}
