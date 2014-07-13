using System;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Model.EntityModels;
using BookArena.Model.ViewModels;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        BookViewModel Book(Expression<Func<Book, bool>> predicate);
        IQueryable<BookViewModel> Books();
        void InsertOrUpdateMetaData(BookMetaData metaData);
        BookMetaData BookMetaData(Expression<Func<BookMetaData, bool>> predicate);
        int AvailableBooks(int bookId);
    }
}