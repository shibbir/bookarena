using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public void InsertOrUpdate(Student entity)
        {
            Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> AllIncluding(params Expression<Func<Student, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Student Find(int id)
        {
            return Find(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Student> All()
        {
            return FindAll();
        }

        public void Save()
        {
            using (var dataContext = DataContext)
            {
                dataContext.SaveChanges();
            }
        }
    }
}