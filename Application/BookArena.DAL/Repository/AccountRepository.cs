using System.Linq;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Repository
{
    public class AccountRepository : RepositoryBase<ApplicationUser>, IAccountRepository
    {
        public ApplicationUserViewModel User()
        {
            using (var context = DataContext)
            {
                return
                    context.ApplicationUser
                        .Select(user => new ApplicationUserViewModel
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Website = user.Website,
                            Address = user.Address,
                            ImageFileName = user.ImageFileName
                        })
                        .FirstOrDefault();
            }
        }

        public ApplicationUserViewModel Login(ApplicationUser applicationUser)
        {
            using (var context = DataContext)
            {
                return
                    context.ApplicationUser.Where(
                        x => x.UserName == applicationUser.UserName && x.Password == applicationUser.Password)
                        .Select(user => new ApplicationUserViewModel
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Website = user.Website,
                            Address = user.Address,
                            ImageFileName = user.ImageFileName
                        })
                        .FirstOrDefault();
            }
        }
    }
}