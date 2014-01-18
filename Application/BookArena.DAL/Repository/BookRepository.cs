using System;
using System.Collections.Generic;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        public bool Create(Book entity)
        {
            return true;
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Book Get(Book entity)
        {
            throw new NotImplementedException();
        }

        public Book GetById(int id)
        {
            return new Book
            {
                Id = 1,
                Title = "ASP.NET MVC 4 Recipes",
                Author = "John Ciliberti",
                Edition = "First Edition",
                ShortDescription = "ASP.NET MVC 4 Recipes is a practical guide for developers creating modern web applications on the Microsoft platform. It cuts through the complexities of ASP.NET, jQuery, Knockout.js and HTML 5 to provide straightforward solutions to common web development problems using proven methods based on best practices.",
                StatusId = 1,
                ImageFileName = "A9781430247739-small_3.png",
                Rating = 3.5
            };
        }

        public IEnumerable<Book> GetAll()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 1,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 2,
                    Title = "Learning jQuery Fourth Edition",
                    Author = "Karl Swedberg and Jonathan Chaffer",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 2,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 3,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Shibbir",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 3,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 4,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 1,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 5,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 2,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 1,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 1,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 2,
                    Title = "Learning jQuery Fourth Edition",
                    Author = "Karl Swedberg and Jonathan Chaffer",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 2,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 3,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 3,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 4,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 1,
                    ImageFileName = "A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 5,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    ShortDescription = "ASP.NET MVC 4",
                    StatusId = 2,
                    ImageFileName = "A9781430247739-small_3.png"
                }
            };
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}