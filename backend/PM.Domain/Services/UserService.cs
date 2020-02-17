using System.Collections.Generic;
using System.Linq;
using PM.Infrastructure;
using PM.Model.Entities;
using static PM.Infrastructure.Helpers.SecurityHelper;

namespace PM.Domain.Services
{

    public class UserService : IUserService
    {
        private readonly IRepositoryWithTypedId<User, int> _userRepo;

        public UserService(IRepositoryWithTypedId<User, int> userRepo)
        {
            _userRepo = userRepo;
        }

        public User Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepo.GetAll().SingleOrDefault(x => x.Login == login);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepo.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepo.GetById(id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_userRepo.GetAll().Any(x => x.Login == user.Login))
                throw new AppException("Login \"" + user.Login + "\" is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepo.Save(user);

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _userRepo.GetById(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (!string.IsNullOrWhiteSpace(userParam.Login) && userParam.Login != user.Login)
            {
                if (_userRepo.GetAll().Any(x => x.Login == userParam.Login))
                    throw new AppException("Login " + userParam.Login + " is already taken");

                user.Login = userParam.Login;
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userRepo.Edit(user);
        }

        public void Delete(int id)
        {
            var user = _userRepo.GetById(id);
            if (user != null)
            {
                _userRepo.Delete(user);
            }
        }
    }
}