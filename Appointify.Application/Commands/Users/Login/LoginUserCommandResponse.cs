namespace Appointify.Application.Commands.Users.Login
{
    public class LoginUserCommandResponse
    {
        public LoginUserCommandResponse(
            string token, 
            string completeName,
            Guid companyId,
            Guid id)
        { 
            Token = token;
            CompleteName = completeName;
            CompanyId = companyId;
            Id = id;
        }

        public string Token { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }

        public Guid Id { get; set; }
    }
}
