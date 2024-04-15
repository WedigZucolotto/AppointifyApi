using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.GetAll
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<GetAllCompaniesResponse>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<GetAllCompaniesResponse>> Handle(GetAllCompaniesQuery query, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository
                .GetAllAsync();

            return companies.Select(
                company => new GetAllCompaniesResponse(
                    company.Id, 
                    company.Name, 
                    company.Plan.Name,
                    company.Open.ToString(@"hh\:mm"),
                    company.Close.ToString(@"hh\:mm")));
        }
    }
}
