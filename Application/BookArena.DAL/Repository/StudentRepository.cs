using System;
using System.Collections.Generic;
using System.Linq;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public void Create(Student entity)
        {
            Add(entity);
        }

        public void Update(Student entity)
        {
            Edit(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetById(int id)
        {
            return Find(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Student> GetAll()
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