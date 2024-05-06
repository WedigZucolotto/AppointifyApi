using MediatR;

namespace Appointify.Application.Queries.Users.Day
{
    public class GetUserDayQuery : IRequest<GetUserDayQueryResponse?>
    {
        public GetUserDayQuery(Guid id, string date)
        {
            Id = id;
            Date = date;
        }

        public Guid Id { get; set; }

        public string Date { get; set; } = string.Empty;
    }
}
