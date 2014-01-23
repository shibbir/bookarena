using System.Linq;
using BookArena.Model;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<Category> Categories();
        //IQueryable<BasicBookViewModel> BooksWithBasicInformation();
    }
}