using MediatR;
using PM.Domain.Models;

namespace PM.Domain.Commands
{
    public class AuthenticateUserCommand : IRequest<UserAuthenticationResult>
    {
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}