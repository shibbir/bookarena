using System;
using System.Collections.Generic;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public void Create(Student entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
                IdCard = "CSE 033 05817",
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
                    IdCard = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCard = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCard = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 4,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCard = "CSE 033 05817",
                    Program = "CSE"
                },
                new Student
                {
                    Id = 5,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    IdCard = "CSE 033 05817",
                    Program = "CSE"
                }
            };
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}