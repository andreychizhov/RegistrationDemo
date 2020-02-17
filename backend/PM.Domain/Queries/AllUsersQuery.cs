using System.Collections.Generic;
using MediatR;
using PM.Domain.Models;

namespace PM.Domain.Queries
{
    public class AllUsersQuery : IRequest<IList<UserModel>>
    {
        
    }
}