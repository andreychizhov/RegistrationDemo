using MediatR;
using PM.Domain.Commands;
using PM.Domain.Services;
using PM.Infrastructure;
using PM.Model.Entities;

namespace PM.Domain.Handlers
{
    public class RegisterUserCommandHandler : RequestHandler<RegisterUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IRepositoryWithTypedId<ClientInfo, int> _clientInfoRepo;
        private readonly IRepositoryWithTypedId<Province, int> _provincesRepo;

        public RegisterUserCommandHandler(IUserService userService,
            IRepositoryWithTypedId<ClientInfo, int> clientInfoRepo,
            IRepositoryWithTypedId<Province, int> provincesRepo)
        {
            _userService = userService;
            _clientInfoRepo = clientInfoRepo;
            _provincesRepo = provincesRepo;
        }
        
        protected override void Handle(RegisterUserCommand request)
        {
            var user = new User {Login = request.Login};
            _userService.Create(user, request.Password);

            var province = _provincesRepo.GetById(request.ProvinceId);
            
            var clientInfo = new ClientInfo
            {
                User = user, 
                Country = province.Country,
                Province = province
            };
            
            _clientInfoRepo.Save(clientInfo);
        }
    }
}