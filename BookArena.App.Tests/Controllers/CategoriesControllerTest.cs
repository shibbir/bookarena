using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.Results;
using BookArena.App.Controllers;
using BookArena.App.Helper;
using BookArena.Data.Interfaces;
using BookArena.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookArena.App.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
        private readonly Mock<ModelFactory> _modelFactory = new Mock<ModelFactory>();
        private readonly CategoriesController _controller;

        public CategoriesControllerTest()
        {
            _controller = new CategoriesController(_categoryRepository.Object, _modelFactory.Object);
        }

        private static IQueryable<Category> GetTestCategories()
        {
            var testStudents = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Title = "PHP",
                    Books = new List<Book>
                    {
                        new Book(),
                        new Book()
                    }
                },
                new Category
                {
                    Id = 2,
                    Title = "ASP",
                    Books = new List<Book>
                    {
                        new Book()
                    }
                }
            };

            return testStudents.AsQueryable();
        }

        private static Category GetTestCategory()
        {
            return new Category
            {
                Id = 1,
                Title = "ASP"
            };
        }

        [TestMethod]
        public void Should_Return_List_Of_Categories()
        {
            _categoryRepository.Setup(x => x.FindAll()).Returns(GetTestCategories());

            var actionResult = _controller.Get();

            var contentResult = actionResult as OkNegotiatedContentResult<Object>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Should_Create_New_Category()
        {
            _categoryRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Category, bool>>>()));

            var actionResult = _controller.Post(GetTestCategory());

            Assert.IsInstanceOfType(actionResult, typeof (OkNegotiatedContentResult<Category>));
        }

        [TestMethod]
        public void Should_Return_BadRequestErrorMessageResult_When_Inserting_Category_With_Existing_Title()
        {
            _categoryRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns(GetTestCategory());

            var actionResult = _controller.Post(GetTestCategory());

            Assert.IsInstanceOfType(actionResult, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Should_Update_Category()
        {
            _categoryRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Category, bool>>>()));

            var actionResult = _controller.Put(GetTestCategory());

            Assert.IsInstanceOfType(actionResult, typeof (OkNegotiatedContentResult<Category>));
        }
    }
}