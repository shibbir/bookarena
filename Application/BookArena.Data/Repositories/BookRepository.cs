using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public void InsertOrUpdate(Book entity)
        {
            if (entity.BookId == default(int))
            {
                _dbContext.Book.Add(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<Book> FindAll(Expression<Func<Book, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Book Find(Expression<Func<Book, bool>> predicate)
        {
            return _dbContext.Book.Where(predicate).FirstOrDefault();
        }

        public IQueryable<Book> FindAll()
        {
            return _dbContext.Book;
        }

        public int AvailableBooks(int bookId)
        {
            return _dbContext.BookMetaData.Count(x => x.BookId == bookId && x.IsAvailable);
        }

        public void InsertOrUpdateMetaData(BookMetaData entity)
        {
            if (entity.Id == default(int))
            {
                _dbContext.BookMetaData.Add(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public BookMetaData BookMetaData(Expression<Func<BookMetaData, bool>> predicate)
        {
            return _dbContext.BookMetaData.Where(predicate).FirstOrDefault();
        }
    }
}