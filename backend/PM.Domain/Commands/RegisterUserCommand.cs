using MediatR;

namespace PM.Domain.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
    }
}