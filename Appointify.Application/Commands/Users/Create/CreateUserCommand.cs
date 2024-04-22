using Appointify.Domain;
using MediatR;

namespace Appointify.Application.Commands.Users.Create
{
    public class CreateUserCommand : IRequest<Nothing>
    {
        public CreateUserCommand(
            string name, 
            string password, 
            string completeName,
            int type,
            Guid companyId,
            bool isOwner)
        {
            Name = name;
            Password = password;
            CompleteName = completeName;
            Type = type;
            CompanyId = companyId;
            IsOwner = isOwner;
        }

        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public int Type { get; set; }

        public Guid CompanyId { get; set; }

        public bool IsOwner { get; set; }
    }
}
