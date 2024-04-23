using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.All
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<GetAllCompaniesQueryResponse>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<GetAllCompaniesQueryResponse>> Handle(GetAllCompaniesQuery query, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetFilteredAsync(query.Name, query.PlanId);

            return companies.Select(
                company => new GetAllCompaniesQueryResponse(
                    company.Id,
                    company.Name,
                    company.Plan.Name,
                    company.Open.ToString(@"hh\:mm"),
                    company.Close.ToString(@"hh\:mm")));
        }
    }
}
