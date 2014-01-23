using BookArena.Model;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        SingleStudentDetailsViewModel StudentDetails(int id);
    }
}