using Cargos.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Cargos.Infrastructure.Base
{
    public class BaseRepository<TEntity> : BaseRepository<TEntity, long, DbContext>
      where TEntity : Domain<long>
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        { }
    }

    public class BaseRepository<TEntity, TKey> : BaseRepository<TEntity, TKey, DbContext>
       where TEntity : Domain<TKey>
       where TKey : IEquatable<TKey>
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        { }
    }

    public class BaseRepository<TEntity, TKey, TDbContext> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomain<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        protected virtual TDbContext DbContext { get; set; }

        public BaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected virtual DbSet<TEntity> EntitySet
        {
            get { return DbContext.Set<TEntity>(); }
        }

        public TEntity GetById(TKey id)
        {
            return GetAll().FirstOrDefault(e => e.Id.Equals(id));
        }

        public IQueryable<TEntity> GetAll()
        {
            return EntitySet;
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }

        public TEntity Save(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            EntitySet.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            EntitySet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();

            return entity;
        }

    }
}
