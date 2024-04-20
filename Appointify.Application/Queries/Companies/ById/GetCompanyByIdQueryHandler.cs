using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, GetCompanyByIdQueryResponse?>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationContext _notification;

        public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository, INotificationContext notification)
        {
            _companyRepository = companyRepository;
            _notification = notification;
        }

        public async Task<GetCompanyByIdQueryResponse?> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(query.Id);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            return new GetCompanyByIdQueryResponse(
                company.Id,
                company.PlanId,
                company.Name,
                company.Open.ToString(@"hh\:mm"),
                company.Close.ToString(@"hh\:mm"));
        }
    }
}
