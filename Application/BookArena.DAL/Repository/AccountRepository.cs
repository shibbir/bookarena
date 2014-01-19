using System.Linq;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class AccountRepository : RepositoryBase<ApplicationUser>, IAccountRepository
    {
        public ApplicationUser User()
        {
            return FindAll().FirstOrDefault();
        }

        public ApplicationUser Login(ApplicationUser applicationUser)
        {
            return
                Find(x => x.UserName == applicationUser.UserName && x.Password == applicationUser.Password)
                    .FirstOrDefault();
        }
    }
}