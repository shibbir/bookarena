using System.Collections.Generic;

namespace BookArena.DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Save();
    }
}