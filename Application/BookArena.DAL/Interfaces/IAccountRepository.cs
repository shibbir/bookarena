using BookArena.Model;

namespace BookArena.DAL.Interfaces
{
    public interface IAccountRepository
    {
        ApplicationUser User(int id);
        bool IsUserAuthenticated();
        bool Login(ApplicationUser applicationUser);
        void LogOut();
    }
}