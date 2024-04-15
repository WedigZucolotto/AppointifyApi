using MediatR;

namespace Appointify.Application.Queries.Companies.GetById
{
    public class GetCompanyByIdQuery : IRequest<GetCompanyByIdQueryResponse?>
    {
        public Guid Id { get; set; }

        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
