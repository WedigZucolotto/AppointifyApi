using Appointify.Domain;
using Appointify.Domain.Authentication;
using Appointify.Domain.Entities;
using Appointify.Domain.Entities.Enums;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using MediatR;

namespace Appointify.Application.Commands.Users.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Nothing>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly INotificationContext _notification;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IPasswordHasher passwordHasher,
            INotificationContext notification)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _passwordHasher = passwordHasher;
            _notification = notification;
        }

        public async Task<Nothing> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByNameAsync(command.Name);

            if (user != null)
            {
                _notification.AddBadRequest("Este nome já está em uso.");
                return default;
            }

            var company = await _companyRepository.GetByIdAsync(command.CompanyId);

            if (company == null)
            {
                _notification.AddNotFound("Empresa não encontrada.");
                return default;
            }

            var hashedPassword = _passwordHasher.Generate(command.Password);

            var newUser = new User(
                command.Name,
                command.CompleteName,
                hashedPassword,
                company);

            if (command.IsOwner)
            {
                newUser.SetTypeToOwner();
            } 
            else
            {
                newUser.SetTypeToEmployee();
            }

            _userRepository.Add(newUser);
            await _userRepository.UnitOfWork.CommitAsync();

            return Nothing.Value;
        }
    }
}
