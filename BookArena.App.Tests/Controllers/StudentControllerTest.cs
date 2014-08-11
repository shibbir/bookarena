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
    public class StudentControllerTest
    {
        [TestMethod]
        public void ShouldReturnPagedResultOfStudents()
        {
            var studentRepository = new Mock<IStudentRepository>();
            var transactionRepository = new Mock<ITransactionRepository>();

            studentRepository.Setup(x => x.FindAll()).Returns(GetTestStudents());

            var controller = new StudentsController(studentRepository.Object, transactionRepository.Object);
            var actionResult = controller.Get(0, 10);

            var contentResult = actionResult as OkNegotiatedContentResult<PagedResult<Student>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
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
        public void Should_Return_NotFoundResult_When_Student_NotFound()
        {
            var studentRepository = new Mock<IStudentRepository>();
            var transactionRepository = new Mock<ITransactionRepository>();

            var controller = new StudentsController(studentRepository.Object, transactionRepository.Object);

            Assert.IsInstanceOfType(controller.Get(99), typeof (NotFoundResult));
            Assert.IsInstanceOfType(controller.Get("xxx-xxx-xxx"), typeof (NotFoundResult));
        }

        [TestMethod]
        public void ShouldReturnStudent()
        {
            var studentRepository = new Mock<IStudentRepository>();
            var transactionRepository = new Mock<ITransactionRepository>();

            studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>())).Returns(GetTestStudent());
            transactionRepository.Setup(x => x.FindAll(It.IsAny<Expression<Func<Transaction, bool>>>()))
                .Returns(GetTestTransactions());

            var controller = new StudentsController(studentRepository.Object, transactionRepository.Object);

            var actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<StudentViewModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void ShouldCreateNewStudent()
        {
            var studentRepository = new Mock<IStudentRepository>();
            var transactionRepository = new Mock<ITransactionRepository>();

            studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>()));

            var controller = new StudentsController(studentRepository.Object, transactionRepository.Object);
            var actionResult = controller.Post(GetTestStudent());

            Assert.IsInstanceOfType(actionResult, typeof (OkResult));
        }

        [TestMethod]
        public void Should_Return_BadRequestErrorMessageResult_When_Inserting_Student_With_Duplicate_IdCard()
        {
            var studentRepository = new Mock<IStudentRepository>();
            var transactionRepository = new Mock<ITransactionRepository>();

            studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>())).Returns(GetTestStudent());

            var controller = new StudentsController(studentRepository.Object, transactionRepository.Object);
            var actionResult = controller.Post(GetTestStudent());

            Assert.IsInstanceOfType(actionResult, typeof (BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void ShouldUpdateStudent()
        {
            var studentRepository = new Mock<IStudentRepository>();
            var transactionRepository = new Mock<ITransactionRepository>();

            studentRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Student, bool>>>()));

            var controller = new StudentsController(studentRepository.Object, transactionRepository.Object);
            var actionResult = controller.Put(GetTestStudent());

            Assert.IsInstanceOfType(actionResult, typeof (OkResult));
        }
    }
}