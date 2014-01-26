using System;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;

namespace BookArena.DAL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly BookArenaDbContext _dbContext;

        public StudentRepository()
        {
            _dbContext = new BookArenaDbContext();
        }

        public void InsertOrUpdate(Student entity)
        {
            _dbContext.Student.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> FindAll(Expression<Func<Student, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Student Find(Expression<Func<Student, bool>> predicate)
        {
            return _dbContext.Student.Where(predicate).FirstOrDefault();
        }

        public IQueryable<Student> FindAll()
        {
            return _dbContext.Student;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}