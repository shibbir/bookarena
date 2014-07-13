using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModels;
using BookArena.Model.ViewModels;

namespace BookArena.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookArenaDbContext _dbContext;

        public BookRepository()
        {
            _dbContext = new BookArenaDbContext();
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
            throw new NotImplementedException();
        }

        public BookViewModel Book(Expression<Func<Book, bool>> predicate)
        {
            var viewModel = _dbContext.Book.Where(predicate).Select(book => new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ShortDescription = book.ShortDescription,
                LongDescription = book.LongDescription,
                ImageFileName = book.ImageFileName,
                Quantity = book.Quantity,
                Rating = book.Rating,
                CategoryTitle = book.Category.Title,
                CategoryId = book.Category.CategoryId
            }).FirstOrDefault();
            if (viewModel != null)
            {
                viewModel.AvailableQuantity = AvailableBooks(viewModel.BookId);
            }
            return viewModel;
        }

        public int AvailableBooks(int bookId)
        {
            return _dbContext.BookMetaData.Count(x => x.BookId == bookId && x.IsAvailable);
        }

        public IQueryable<BookViewModel> Books()
        {
            return _dbContext.Book.Select(book => new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                ImageFileName = book.ImageFileName,
            });
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