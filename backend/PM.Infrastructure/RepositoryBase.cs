using System;
using System.Linq;

namespace PM.Infrastructure
{
    public abstract class RepositoryBase<TEntity, TId> : IRepositoryWithTypedId<TEntity, TId> where TEntity : Entity<TId>
    {
        private readonly DbSessionBase _dbSession;

        protected RepositoryBase(DbSessionBase dbSession)
        {
            if (dbSession == null)
            {
                throw new ArgumentNullException("dbSession");
            }
            _dbSession = dbSession;
        }

        public abstract TEntity GetById(TId id);

        public abstract IQueryable<TEntity> GetAll();

        public abstract void Delete(TEntity target);

        protected abstract void SaveCore(TEntity entity);

        public void Save(TEntity entity)
        {
            SaveCore(entity);
        }

        protected abstract void EditCore(TEntity entity);

        public IQueryable<TEntity> GetAllBySpecification(ISimpleSpecification<TEntity> specification)
        {
            return GetAll().Where(specification.IsSatisfiedBy());
        }

        public virtual TEntity GetBySpecification(ISimpleSpecification<TEntity> specification)
        {
            return GetAll().Where(specification.IsSatisfiedBy()).SingleOrDefault();
        }

        public void Edit(TEntity entity)
        {
            EditCore(entity);
        }

        public void Delete(TId id)
        {
            TEntity entity = GetById(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }
    }
}