using System;
using System.Collections.Generic;
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
            Reomve(GetById(id));
        }

        public Student Get(Student entity)
        {
            throw new NotImplementedException();
        }

        public Student GetById(int id)
        {
            return new Student
            {
                Id = 1,
                FirstName = "Shibbir",
                LastName = "Ahmed",
                Batch = "33",
                IdCardNumber = "CSE 033 05817",
                Program = "CSE"
            };
        }

        public IEnumerable<Student> GetAll()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCardNumber = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCardNumber = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCardNumber = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 4,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCardNumber = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 5,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCardNumber = "CSE 033 05817",
                    Program = "CSE"
                }
            };
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