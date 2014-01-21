using System;
using System.Collections.Generic;
using System.Linq;
using BookArena.DAL.Interfaces;
using BookArena.Model;
using BookArena.Model.ViewModel;

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

        public Student Find(int id)
        {
            return Find(x => x.Id == id).FirstOrDefault();
        }

        public SingleStudentDetailsViewModel StudentDetails(int id)
        {
            var student = Find(id);
            return new SingleStudentDetailsViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Batch = student.Batch,
                Program = student.Program,
                IdCardNumber = student.IdCardNumber,
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
                        StatusId = 2
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
                        StatusId = 1
                    }
                }
            };
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