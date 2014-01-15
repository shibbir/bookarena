﻿using System;
using System.Collections.Generic;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        public void Create(Book entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                    Description = "ASP.NET MVC 4",
                    StatusText = GetBookStatus(1),
                    ImageFileName = "http://www.apress.com/media/catalog/product/cache/9/small_image/125x159/040ec09b1e35df139433887a97daa66f/A/9/A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 2,
                    Title = "Learning jQuery Fourth Edition",
                    Author = "Karl Swedberg and Jonathan Chaffer",
                    Edition = "First Edition",
                    Description = "ASP.NET MVC 4",
                    StatusText = GetBookStatus(2),
                    ImageFileName = "http://www.apress.com/media/catalog/product/cache/9/small_image/125x159/040ec09b1e35df139433887a97daa66f/A/9/A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 3,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    Description = "ASP.NET MVC 4",
                    StatusText = GetBookStatus(3),
                    ImageFileName = "http://www.apress.com/media/catalog/product/cache/9/small_image/125x159/040ec09b1e35df139433887a97daa66f/A/9/A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 4,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    Description = "ASP.NET MVC 4",
                    StatusText = GetBookStatus(1),
                    ImageFileName = "http://www.apress.com/media/catalog/product/cache/9/small_image/125x159/040ec09b1e35df139433887a97daa66f/A/9/A9781430247739-small_3.png"
                },
                new Book
                {
                    Id = 5,
                    Title = "Beginning ASP.NET MVC 4",
                    Author = "Scott Allen",
                    Edition = "First Edition",
                    Description = "ASP.NET MVC 4",
                    StatusText = GetBookStatus(1),
                    ImageFileName = "http://www.apress.com/media/catalog/product/cache/9/small_image/125x159/040ec09b1e35df139433887a97daa66f/A/9/A9781430247739-small_3.png"
                }
            };
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private string GetBookStatus(int statusId)
        {
            if (statusId == 1) return "Available";
            return statusId == 2 ? "Not Available" : "Rented";
        }
    }
}