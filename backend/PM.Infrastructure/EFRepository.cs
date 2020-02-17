using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PM.Infrastructure
{
    public class EFRepository<TEntity, TId> : RepositoryBase<TEntity, TId>
        where TEntity : Entity<TId>
    {
        private readonly DbContext _context;

        private DbSet<TEntity> Entities
        {
            get { return _context.Set<TEntity>(); }
        }

        public EFRepository(IDbSession context)
            : base(context as DbSessionBase)
        {
            var dbSession = context as EFDbSession;
            if (dbSession == null)
            {
                throw new ArgumentException("Uncompatible dbSession, EFDbSession is expecting");
            }
            _context = dbSession.Context;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Entities.AsQueryable();
        }

        public override TEntity GetById(TId id)
        {
            return Entities.Find(id);
        }

        protected override void SaveCore(TEntity entity)
        {
            Entities.Add(entity);
            if (!entity.IsTransient())
            {
                var previousEntity = GetById(entity.Id);
                _context.Entry(previousEntity).State = EntityState.Detached;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        protected override void EditCore(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}
