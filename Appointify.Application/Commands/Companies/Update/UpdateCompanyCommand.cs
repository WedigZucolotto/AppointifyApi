using Appointify.Domain;
using MediatR;
using System.Text.Json.Serialization;

namespace Appointify.Application.Commands.Companies.Update
{
    public class UpdateCompanyCommand : IRequest<Nothing>
    {
        public UpdateCompanyCommand WithId(Guid id)
        {
            Id = id;
            return this;
        }

        [JsonIgnore]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Open { get; set; }

        public string? Close { get; set; }

        public Guid? PlanId { get; set; }
    }
}
