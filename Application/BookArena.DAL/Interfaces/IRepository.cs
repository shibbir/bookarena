using System.Linq;

namespace BookArena.DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();
        TEntity Find(int id);
        void InsertOrUpdate(TEntity entity);
        void Delete(int id);
        void Save();
    }
}