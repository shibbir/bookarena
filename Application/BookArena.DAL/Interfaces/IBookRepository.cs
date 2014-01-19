using System.Collections.Generic;
using BookArena.Model;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Category> Categories();
    }
}