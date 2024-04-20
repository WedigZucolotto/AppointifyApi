using MediatR;

namespace Appointify.Application.Queries.Companies.ToSchedule
{
    public class GetCompanyToScheduleQuery : IRequest<GetCompanyToScheduleQueryResponse?>
    {
        public GetCompanyToScheduleQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
