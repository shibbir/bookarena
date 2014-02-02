using System;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        BookViewModel Book(Expression<Func<Book, bool>> predicate);
        IQueryable<BookViewModel> LatestBooks(int limit);
        void InsertOrUpdateMetaData(BookMetaData metaData);
        BookMetaData BookMetaData(Expression<Func<BookMetaData, bool>> predicate);
        int AvailableBooks(int bookId);
    }
}