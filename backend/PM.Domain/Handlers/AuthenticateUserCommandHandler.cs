using MediatR;
using PM.Domain.Commands;
using PM.Domain.Models;
using PM.Domain.Services;

namespace PM.Domain.Handlers
{
    public class AuthenticateUserCommandHandler : RequestHandler<AuthenticateUserCommand, UserAuthenticationResult>
    {
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        protected override UserAuthenticationResult Handle(AuthenticateUserCommand request)
        {
            var user = _userService.Authenticate(request.Login, request.Password);
            
            return new UserAuthenticationResult
            {
                Id = user?.Id ?? 0, 
                Login = user?.Login ?? string.Empty,
                IsSuccess = user != null
            };
        }
    }
}