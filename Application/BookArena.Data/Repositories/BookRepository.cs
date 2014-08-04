using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public int AvailableBooks(int bookId)
        {
            return Context.BookMetaData.Count(x => x.BookId == bookId && x.IsAvailable);
        }

        public void InsertOrUpdateMetaData(BookMetaData entity)
        {
            if (entity.Id == default(int))
            {
                Context.BookMetaData.Add(entity);
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public BookMetaData BookMetaData(Expression<Func<BookMetaData, bool>> predicate)
        {
            return Context.BookMetaData.Where(predicate).FirstOrDefault();
        }
    }
}