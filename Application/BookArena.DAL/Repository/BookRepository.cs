using System;
using System.Collections.Generic;
using System.Linq;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public void Create(Book entity)
        {
            Add(entity);
        }

        public void Update(Book entity)
        {
            Edit(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetById(int id)
        {
            using (var context = DataContext)
            {
                var book = context.Book.FirstOrDefault(x => x.Id == id);
                var category = context.Category.FirstOrDefault(x => x.Id == book.CategoryId);

                if (book != null && category != null)
                {
                    book.CategoryTitle = category.Title;
                }
                return book;
            }
        }

        public IEnumerable<Book> GetAll()
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

        public IEnumerable<Category> Categories()
        {
            return DataContext.Category.ToList();
        }
    }
}