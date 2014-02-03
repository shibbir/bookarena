using System;
using BookArena.DAL.Interfaces;

namespace BookArena.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookArenaDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new BookArenaDbContext();
        }

        public UnitOfWork(BookArenaDbContext bookArenaDbContext)
        {
            _dbContext = bookArenaDbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        internal BookArenaDbContext Context
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}