using BookArena.Model;

namespace BookArena.DAL.Interfaces
{
    public interface IAccountRepository
    {
        ApplicationUserViewModel User();
        ApplicationUserViewModel Login(ApplicationUser applicationUser);
    }
}