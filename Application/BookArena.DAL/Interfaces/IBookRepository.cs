using System;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<BookViewModel> Books();
        BookViewModel Book(Expression<Func<Book, bool>> predicate);
        IQueryable<BookViewModel> Books(Expression<Func<Book, bool>> predicate);
        IQueryable<BookViewModel> LatestBooks(int limit);
    }
}