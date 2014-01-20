using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model;

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

        public IQueryable<Book> AllIncluding(params Expression<Func<Book, object>>[] includeProperties)
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