﻿namespace Appointify.Application.Commands.Users.Login
{
    public class LoginUserCommandResponse
    {
        public LoginUserCommandResponse(
            string token, 
            string completeName,
            Guid companyId,
            Guid id,
            bool isOwner)
        { 
            Token = token;
            CompleteName = completeName;
            CompanyId = companyId;
            Id = id;
            IsOwner = isOwner;
        }

        public string Token { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }

        public Guid Id { get; set; }

        public bool IsOwner { get; set; }
    }
}
