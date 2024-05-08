using Appointify.Application.Queries.Dtos;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Queries.Services.Options
{
    public class GetServiceOptionsQueryHandler : IRequestHandler<GetServiceOptionsQuery, IEnumerable<OptionDto>?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContext _httpContext;
        private readonly INotificationContext _notification;

        public GetServiceOptionsQueryHandler(
            IServiceRepository serviceRepository, 
            IUserRepository userRepository,
            IHttpContext httpContext,
            INotificationContext notification)
        {
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _httpContext = httpContext;
            _notification = notification;
        }

        public async Task<IEnumerable<OptionDto>?> Handle(GetServiceOptionsQuery query, CancellationToken cancellationToken)
        {
            var userClaims = _httpContext.GetUserClaims();

            if (userClaims.Id != query.UserId)
            {
                _notification.AddUnauthorized("Você não tem permissão para realizar essa operação.");
                return default;
            }
            
            var user = await _userRepository.GetByIdAsync(query.UserId);

            if (user == null)
            {
                _notification.AddNotFound("Usuário não encontrado.");
                return default;
            }

            var services = await _serviceRepository.GetFilteredAsync(user.CompanyId, null);
            return services.Select(s => new OptionDto(s.Name, s.Id));
        }
    }
}
