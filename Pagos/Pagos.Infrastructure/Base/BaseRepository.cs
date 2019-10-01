using Microsoft.EntityFrameworkCore;
using Pagos.Domain.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pagos.Infrastructure.Base
{
    public class BaseRepository<TEntity> : BaseRepository<TEntity, long, ApplicationDBContext>
        where TEntity : Domain<long>
    {
        public BaseRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        { }
    }

    public class BaseRepository<TEntity, TKey> : BaseRepository<TEntity, TKey, ApplicationDBContext>
        where TEntity : Domain<TKey>
        where TKey : IEquatable<TKey>
    {
        public BaseRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        { }
    }

    public class BaseRepository<TEntity, TKey, TApplicationDbContext> : IBaseRepository<TEntity, TKey>
       where TEntity : class, IDomain<TKey>
       where TKey : IEquatable<TKey>
       where TApplicationDbContext : ApplicationDBContext
    {
        protected virtual TApplicationDbContext ApplicationDBContext { get; set; }

        public BaseRepository(TApplicationDbContext applicationDBContext)
        {
            ApplicationDBContext = applicationDBContext;
        }

        protected virtual DbSet<TEntity> EntitySet
        {
            get { return ApplicationDBContext.Set<TEntity>(); }
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
            ApplicationDBContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            EntitySet.Attach(entity);
            ApplicationDBContext.Entry(entity).State = EntityState.Modified;
            ApplicationDBContext.SaveChanges();

            return entity;
        }
    }
}
