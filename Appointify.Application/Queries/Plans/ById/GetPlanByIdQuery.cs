using MediatR;

namespace Appointify.Application.Queries.Plans.ById
{
    public class GetPlanByIdQuery : IRequest<GetPlanByIdQueryResponse?>
    {
        public GetPlanByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
