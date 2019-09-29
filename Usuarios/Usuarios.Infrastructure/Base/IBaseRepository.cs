using System;
using System.Linq;
using System.Linq.Expressions;
using Usuarios.Domain.Base;

namespace Usuarios.Infrastructure.Base
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, long> where TEntity : IDomain<long>
    { }

    public interface IBaseRepository<TEntity, TKey>
     where TEntity : IDomain<TKey>
     where TKey : IEquatable<TKey>
    {
        TEntity GetById(TKey id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        TEntity Save(TEntity entity);

        TEntity Update(TEntity entity);

    }
}
