namespace Appointify.Application.Commands.Users.Login
{
    public class LoginUserCommandResponse
    {
        public LoginUserCommandResponse(string token, Guid id) 
        { 
            Token = token;
            Id = id;
        }

        public string Token { get; set; } = string.Empty;

        public Guid Id { get; set; }
    }
}
