using System.Linq;
using MediatR;
using PM.Domain.Models;
using PM.Domain.Queries;
using PM.Domain.Services;
using PM.Infrastructure;
using PM.Model.Entities;

namespace PM.Domain.Handlers
{
    public class UserByIdQueryHandler : RequestHandler<UserByIdQuery, UserModel>
    {
        private readonly IUserService _userService;
        private readonly IRepositoryWithTypedId<ClientInfo, int> _clientInfoRepo;

        public UserByIdQueryHandler(IUserService userService, IRepositoryWithTypedId<ClientInfo, int> clientInfoRepo)
        {
            _userService = userService;
            _clientInfoRepo = clientInfoRepo;
        }
        
        protected override UserModel Handle(UserByIdQuery request)
        {
            var user = _userService.GetById(request.UserId);

            var info = _clientInfoRepo.GetAll()
                .FirstOrDefault(i => i.User.Id == request.UserId);
            
            return new UserModel
            {
                Id = user.Id,
                Login = user.Login,
                Country = info?.Country?.Name ?? "Not set",
                Province = info?.Province?.Name ?? "Not set"
            };
        }
    }
}