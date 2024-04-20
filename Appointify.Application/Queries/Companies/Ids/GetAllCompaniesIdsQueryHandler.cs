using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.Ids
{
    public class GetAllCompaniesIdsQueryHandler : IRequestHandler<GetAllCompaniesIdsQuery, IEnumerable<Guid>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesIdsQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Guid>> Handle(GetAllCompaniesIdsQuery query, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync();
            return companies.Select(company => company.Id);
        }
    }
}
