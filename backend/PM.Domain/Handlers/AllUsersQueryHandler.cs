using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PM.Domain.Models;
using PM.Domain.Queries;
using PM.Domain.Services;
using PM.Infrastructure;
using PM.Model.Entities;

namespace PM.Domain.Handlers
{
    public class AllUsersQueryHandler : IRequestHandler<AllUsersQuery, IList<UserModel>>
    {
        private readonly IRepositoryWithTypedId<User, int> _userRepo;
        private readonly IRepositoryWithTypedId<ClientInfo, int> _clientInfoRepo;
        private readonly IRepositoryWithTypedId<Country, int> _countryRepo;
        private readonly IRepositoryWithTypedId<Province, int> _provinceRepo;

        public AllUsersQueryHandler(IRepositoryWithTypedId<User, int> userRepo, 
            IRepositoryWithTypedId<ClientInfo, int> clientInfoRepo, 
            IRepositoryWithTypedId<Country, int> countryRepo,
        IRepositoryWithTypedId<Province, int> provinceRepo)
        {
            _userRepo = userRepo;
            _clientInfoRepo = clientInfoRepo;
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
        }
        
        public async Task<IList<UserModel>> Handle(AllUsersQuery request, CancellationToken token)
        {
            var usersQuery = 
                from u in _userRepo.GetAll()
                join c in _clientInfoRepo.GetAll() on u.Id equals c.User.Id into gj
                from info in gj.DefaultIfEmpty()
                join cn in _countryRepo.GetAll() on info.CountryId equals cn.Id
                join p in _provinceRepo.GetAll() on info.ProvinceId equals p.Id 
                select new UserModel { Id = u.Id, Login = u.Login, Country = cn.Name, Province = p.Name};

            var users = await usersQuery.ToListAsync(token);

            var result = users.Select(x => new UserModel
            {
                Id = x.Id,
                Login = x.Login,
                Country = x.Country ?? "Not set",
                Province = x.Province ?? "Not set"
            }).ToList();

            return result;
        }
    }
}