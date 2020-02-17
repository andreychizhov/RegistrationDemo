using System.Linq;

namespace PM.Infrastructure
{
    public interface IRepositoryWithTypedId { }
    public interface IRepositoryWithTypedId<T, TId> : IRepositoryWithTypedId
    {
        void Delete(T target);

        void Delete(TId id);

        void Save(T entity);

        void Edit(T entity);

        T GetById(TId id);

        T GetBySpecification(ISimpleSpecification<T> specification);

        IQueryable<T> GetAll();

        IQueryable<T> GetAllBySpecification(ISimpleSpecification<T> specification);
    }
}