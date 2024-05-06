using MediatR;

namespace Appointify.Application.Queries.Users.Month
{
    public class GetUserMonthQuery : IRequest<IEnumerable<GetUserMonthQueryResponse>?>
    {
        public GetUserMonthQuery(Guid id, string date)
        {
            Id = id;
            Date = date;
        }

        public Guid Id { get; set; }

        public string Date { get; set; } = string.Empty;
    }
}
