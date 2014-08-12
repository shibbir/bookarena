using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.Results;
using BookArena.App.Controllers;
using BookArena.App.ViewModels;
using BookArena.Core;
using BookArena.Data.Interfaces;
using BookArena.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookArena.App.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerTest
    {
        private readonly Mock<IStudentRepository> _studentRepository = new Mock<IStudentRepository>();
        private readonly Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
        private readonly StudentsController _controller;

        public StudentsControllerTest()
        {
            _controller = new StudentsController(_studentRepository.Object, _transactionRepository.Object);
        }

        private static IQueryable<Student> GetTestStudents()
        {
            var testStudents = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    FirstName = "Shibbir",
                    LastName = "Ahmed",
                    Batch = "33",
                    Program = "CSE",
                    IdCardNumber = "03305817"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    Batch = "45",
                    Program = "CEN",
                    IdCardNumber = "04505817"
                }
            };

            return testStudents.AsQueryable();
        }

        private static IQueryable<Transaction> GetTestTransactions()
        {
            var testTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = 1,
                    IsActive = true
                },
                new Transaction
                {
                    Id = 2,
                    IsActive = false
                }
            };

            return testTransactions.AsQueryable();
        }

        private static Student GetTestStudent()
        {
            return new Student
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Batch = "420",
                Program = "ENG",
                IdCardNumber = "04505817"
            };
        }

        [TestMethod]
        public void Should_Return_PagedResult_Of_Students()
        {
            _studentRepository.Setup(x => x.FindAll()).Returns(GetTestStudents());

            var actionResult = _controller.Get(0, 10);

            var contentResult = actionResult as OkNegotiatedContentResult<PagedResult<Student>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Should_Return_NotFoundResult_When_Student_NotFound()
        {
            Assert.IsInstanceOfType(_controller.Get(99), typeof (NotFoundResult));
            Assert.IsInstanceOfType(_controller.Get("xxx-xxx-xxx"), typeof (NotFoundResult));
        }

        [TestMethod]
        public void Should_Return_Student()
        {
            _studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>())).Returns(GetTestStudent());
            _transactionRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns(GetTestTransactions());

            var actionResult = _controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<StudentViewModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void Should_Create_New_Student()
        {
            _studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>()));

            var actionResult = _controller.Post(GetTestStudent());

            Assert.IsInstanceOfType(actionResult, typeof (OkResult));
        }

        [TestMethod]
        public void Should_Return_BadRequestErrorMessageResult_When_Inserting_Student_With_Existing_IdCard()
        {
            _studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>())).Returns(GetTestStudent());

            var actionResult = _controller.Post(GetTestStudent());

            Assert.IsInstanceOfType(actionResult, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void Should_Update_Student()
        {
            _studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>()));

            var actionResult = _controller.Put(GetTestStudent());

            Assert.IsInstanceOfType(actionResult, typeof (OkResult));
        }
    }
}