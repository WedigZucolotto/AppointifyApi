using Appointify.Application.Queries.Dtos;
using MediatR;

namespace Appointify.Application.Queries.Companies.AvailableTimes
{
    public class GetAvailableTimesQuery : IRequest<IEnumerable<AvailableTimeDto>?>
    {
        public GetAvailableTimesQuery WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public Guid Id { get; set; }

        public string Date { get; set; } = string.Empty;

        public Guid ServiceId { get; set; }

        public Guid? UserId { get; set; }
    }
}
