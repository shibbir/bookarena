using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.Results;
using BookArena.App.Controllers;
using BookArena.App.Helper;
using BookArena.App.ViewModels;
using BookArena.Data.Interfaces;
using BookArena.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookArena.App.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        private readonly Mock<IBookRepository> _bookRepository = new Mock<IBookRepository>();
        private readonly Mock<IStudentRepository> _studentRepository = new Mock<IStudentRepository>();
        private readonly Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
        private readonly Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
        private readonly Mock<ModelFactory> _modelFactory = new Mock<ModelFactory>();
        private readonly BooksController _controller;

        public BooksControllerTest()
        {
            _controller = new BooksController(_bookRepository.Object, _studentRepository.Object,
                _categoryRepository.Object, _transactionRepository.Object, _modelFactory.Object);
        }

        private static IQueryable<Book> GetTestBooks()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "ASP Jump Start",
                    ImageFileName = "image1.png"
                },
                new Book
                {
                    Id = 2,
                    Title = "PHP Jump Start",
                    ImageFileName = "image2.png"
                }
            }.AsQueryable();
        }

        private static Book GetTestBook()
        {
            return new Book
            {
                Id = 1,
                Title = "ASP Jump Start",
                ImageFileName = "image1.png"
            };
        }

        [TestMethod]
        public void Should_Return_Atmost_Five_BasicBookViewModel()
        {
            _bookRepository.Setup(x => x.FindAll()).Returns(GetTestBooks());
            _bookRepository.Setup(x => x.AvailableBooks(It.IsAny<int>())).Returns(5);

            var actionResult = _controller.Get();

            var contentResult = actionResult as OkNegotiatedContentResult<List<BasicBookViewModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(5 >= contentResult.Content.Count);
        }

        [TestMethod]
        public void Should_Return_Book()
        {
            _bookRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Book, bool>>>())).Returns(GetTestBook());
            _bookRepository.Setup(x => x.AvailableBooks(It.IsAny<int>())).Returns(5);

            var actionResult = _controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<BookViewModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void Should_Create_New_Book()
        {
            _bookRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Book, bool>>>()));

            var actionResult = _controller.Post(GetTestBook());

            Assert.IsInstanceOfType(actionResult, typeof (OkResult));
        }

        [TestMethod]
        public void Should_Return_BadRequestErrorMessageResult_When_Inserting_Book_With_Existing_Title()
        {
            _bookRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Book, bool>>>())).Returns(GetTestBook());

            var actionResult = _controller.Post(GetTestBook());

            Assert.IsInstanceOfType(actionResult, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Should_Update_Book()
        {
            _bookRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Book, bool>>>()));

            var actionResult = _controller.Put(GetTestBook());

            Assert.IsInstanceOfType(actionResult, typeof (OkResult));
        }
    }
}