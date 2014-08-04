using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using BookArena.App.Controllers;
using BookArena.Core;
using BookArena.Data.Interfaces;
using BookArena.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

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
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Batch = "40",
                    Program = "BBA",
                    IdCardNumber = "04005817"
                }
            };

            return testStudents.AsQueryable();
        }
    }
}