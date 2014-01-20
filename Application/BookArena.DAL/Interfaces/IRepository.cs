using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookArena.DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity Find(int id);
        void InsertOrUpdate(TEntity entity);
        void Delete(int id);
        void Save();
    }
}