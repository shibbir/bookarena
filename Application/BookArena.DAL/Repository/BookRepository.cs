using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public void InsertOrUpdate(Book entity)
        {
            if (entity.BookId == default(int))
            {
                Add(entity);
            }
            else
            {
                Edit(entity);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Book Find(int id)
        {
            return DataContext.Book.Where(x => x.BookId == id).Include(p => p.Category).FirstOrDefault();
        }

        public IQueryable<Book> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BasicBookViewModel> LatestBooks(int limit)
        {
            var latestBooks = FindAll().Select(book => new BasicBookViewModel
            {
                Id = book.BookId,
                Title = book.Title,
                ImageFileName = book.ImageFileName,
                StatusId = book.StatusId
            }).OrderByDescending(x => x.Id).Take(limit).ToList();
            return latestBooks;
        }

        public void Save()
        {
            using (var dataContext = DataContext)
            {
                dataContext.SaveChanges();
            }
        }

        public IQueryable<Category> Categories()
        {
            return DataContext.Category;
        }
    }
}