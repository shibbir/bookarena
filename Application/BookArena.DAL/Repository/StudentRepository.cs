using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

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

        public StudentViewModel StudentViewModel(Expression<Func<Student, bool>> predicate)
        {
            var student = _dbContext.Student.Where(predicate).Select(s => new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Batch = s.Batch,
                Program = s.Program,
                IdCardNumber = s.IdCardNumber,
                Books = new List<Book>
                {
                    new Book
                    {
                        BookId = 1,
                        Title = "Objective-C Programmer's Reference",
                        Author = "Carlos Oliveira",
                        ShortDescription =
                            "Objective-C Programmer's Reference is a swift and to-the-point reference for professional programmers to the language of choice in developing applications for iOS and OSX.",
                        ImageFileName = "apple_1.png",
                        Rating = 3.5,
                        Quantity = 2
                    },
                    new Book
                    {
                        BookId = 2,
                        Title = "ASP.NET MVC 4 Recipes",
                        Author = "John Ciliberti",
                        ShortDescription =
                            "ASP.NET MVC 4 Recipes is a practical guide for developers creating modern web applications on the Microsoft platform. It cuts through the complexities of ASP.NET, jQuery, Knockout.js and HTML 5 to provide straightforward solutions to common web development problems using proven methods based on best practices.",
                        ImageFileName = "asp_2.png",
                        Rating = 3,
                        Quantity = 1
                    }
                }
            }).FirstOrDefault();
            return student;
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