using BookArena.DAL.Interfaces;

namespace BookArena.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookArenaDbContext _context;

        public UnitOfWork()
        {
            _context = new BookArenaDbContext();
        }

        public UnitOfWork(BookArenaDbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        internal BookArenaDbContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}