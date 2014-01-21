using BookArena.Model;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IAccountRepository
    {
        ApplicationUserViewModel User();
        ApplicationUserViewModel Login(ApplicationUser applicationUser);
    }
}