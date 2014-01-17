using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookArena.DAL
{
    public abstract class RepositoryBase<TEntity> : IDisposable
        where TEntity : class
    {
        private BookArenaDbContext _dataContext;
        private readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase()
        {
            _dataContext = new BookArenaDbContext();
            _dbSet = _dataContext.Set<TEntity>();
        }

        protected virtual BookArenaDbContext DataContext
        {
            get
            {
                if (_dataContext != null) return _dataContext;
                _dataContext = new BookArenaDbContext();
                AllowSerialization = true;
                return _dataContext;
            }
        }

        protected virtual bool AllowSerialization
        {
            get { return _dataContext.Configuration.ProxyCreationEnabled; }
            set { _dataContext.Configuration.ProxyCreationEnabled = !value; }
        }

        public virtual void Add(TEntity newEntity)
        {
            _dbSet.Add(newEntity);
        }

        public virtual void Reomve(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Edit(TEntity updatedEntity)
        {
            var dbEntityEntry = _dataContext.Entry(updatedEntity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                _dbSet.Attach(updatedEntity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual IQueryable<TEntity> FindAll()
        {
            return _dbSet;
        }

        public virtual void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}