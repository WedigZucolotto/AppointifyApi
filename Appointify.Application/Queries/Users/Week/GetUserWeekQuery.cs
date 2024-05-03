using MediatR;

namespace Appointify.Application.Queries.Users.Week
{
    public class GetUserWeekQuery : IRequest<IEnumerable<GetUserWeekQueryResponse>?>
    {
        public GetUserWeekQuery(Guid id, string date)
        {
            Id = id;
            Date = date;
        }

        public Guid Id { get; set; }

        public string Date { get; set; } = string.Empty;
    }
}
