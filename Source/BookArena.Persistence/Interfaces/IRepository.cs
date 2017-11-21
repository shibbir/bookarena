using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookArena.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Save();

        TEntity Find(object id);
        TEntity Find(Expression<Func<TEntity, bool>> filter);
        TEntity FindIncluding(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> FindAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}