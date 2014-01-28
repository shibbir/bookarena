using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;

namespace BookArena.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookArenaDbContext _dbContext;

        public CategoryRepository()
        {
            _dbContext = new BookArenaDbContext();
        }

        public void InsertOrUpdate(Category entity)
        {
            if (entity.CategoryId == default(int))
            {
                _dbContext.Category.Add(entity);
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

        public IQueryable<Category> FindAll(Expression<Func<Category, bool>> predicate)
        {
            return _dbContext.Category.Where(predicate);
        }

        public Category Find(Expression<Func<Category, bool>> predicate)
        {
            return _dbContext.Category.Where(predicate).FirstOrDefault();
        }

        public IQueryable<Category> FindAll()
        {
            return _dbContext.Category;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}