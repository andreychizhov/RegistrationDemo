using System;
using System.Linq.Expressions;

namespace PM.Infrastructure
{
    public interface ISimpleSpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy();
    }
}