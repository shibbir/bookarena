using System;
using System.Linq.Expressions;
using BookArena.Model;

namespace BookArena.Data.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        void InsertOrUpdateMetaData(BookMetaData metaData);
        BookMetaData BookMetaData(Expression<Func<BookMetaData, bool>> predicate);
        int AvailableBooks(int bookId);
    }
}