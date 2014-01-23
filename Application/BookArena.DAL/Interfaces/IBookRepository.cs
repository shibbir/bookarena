using System.Collections.Generic;
using System.Linq;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<Category> Categories();
        IEnumerable<BasicBookViewModel> LatestBooks(int limit);
    }
}