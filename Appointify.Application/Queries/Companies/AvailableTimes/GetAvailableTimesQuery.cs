using MediatR;

namespace Appointify.Application.Queries.Companies.AvailableTimes
{
    public class GetAvailableTimesQuery : IRequest<IEnumerable<string>>
    {
        public GetAvailableTimesQuery WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public Guid Id { get; set; }

        public string Date { get; set; }

        public Guid ServiceId { get; set; }

        public Guid? UserId { get; set; }
    }
}
