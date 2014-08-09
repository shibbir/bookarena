using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using BookArena.App.Controllers;
using BookArena.Core;
using BookArena.Data.Interfaces;
using BookArena.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace BookArena.App.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        [TestMethod]
        public void GetAllStudents_ShouldReturnAllStudents()
        {
            var testModel = Pagination<Student>.GetPagedData(GetTestStudents().OrderBy(x => x.Id), 0, 10);

            var testResult = new
            {
                Data = testModel,
                CurrentPage = 0
            };

            var studentRepository = Mock.Create<IStudentRepository>();
            var transactionRepository = Mock.Create<ITransactionRepository>();

            Mock.Arrange(() => studentRepository.FindAll()).Returns(GetTestStudents());

            var controller = new StudentsController(studentRepository, transactionRepository);

            var result = controller.Get(0, 10) as OkNegotiatedContentResult<object>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testResult, result.Content);
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
        public void GetReturnsNotFound()
        {
            var studentRepository = Mock.Create<IStudentRepository>();
            var transactionRepository = Mock.Create<ITransactionRepository>();

            var students = Mock.Create<IQueryable<Student>>();

            studentRepository.Arrange(x => x.FindAll()).Returns(students);
            students.Arrange(x => x).Returns(GetTestStudent());

            var controller = new StudentsController(studentRepository, transactionRepository);

            var actionResult = controller.Get(9999);

            Assert.IsInstanceOfType(actionResult, typeof (NotFoundResult));
        }
    }
}