using MediatR;
using PM.Domain.Models;

namespace PM.Domain.Queries
{
    public class UserByIdQuery : IRequest<UserModel>
    {
        public UserByIdQuery(int userId)
        {
            UserId = userId;
        }
        
        public int UserId { get; }
    }
}