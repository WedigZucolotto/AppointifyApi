using MediatR;

namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQuery : IRequest<GetCompanyByIdQueryResponse?>
    {
        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
