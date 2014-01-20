using System.Linq;
using BookArena.Model;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<Category> Categories();
    }
}