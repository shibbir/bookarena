using BookArena.Model;

namespace BookArena.DAL.Interfaces
{
    public interface IAccountRepository
    {
        ApplicationUser User();
        ApplicationUser Login(ApplicationUser applicationUser);
    }
}