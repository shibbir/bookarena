using System;
using System.Linq.Expressions;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        StudentViewModel StudentViewModel(Expression<Func<Student, bool>> predicate);
    }
}