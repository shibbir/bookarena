using System;
using System.Collections.Generic;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class AccountRepository : RepositoryBase<ApplicationUser>, IAccountRepository
    {
        public ApplicationUser User()
        {
            return new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                Password = "123456",
                Name = "Shibbir Ahmed",
                Email = "shibbir.cse@gmail.com",
                Address = "Rampura, Dhaka"
            };
        }

        public ApplicationUser Login(ApplicationUser applicationUser)
        {
            if (applicationUser.UserName == "admin" && applicationUser.Password == "123456")
            {
                return new ApplicationUser
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "123456",
                    Name = "Shibbir Ahmed",
                    Email = "shibbir.cse@gmail.com",
                    Address = "Rampura, Dhaka"
                };
            }
            return null;
        }
    }
}