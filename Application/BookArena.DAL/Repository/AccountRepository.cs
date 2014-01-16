using System;
using System.Collections.Generic;
using BookArena.DAL.Interfaces;
using BookArena.Model;

namespace BookArena.DAL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public ApplicationUser User(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsUserAuthenticated()
        {
            return false;
        }

        public bool Login(ApplicationUser applicationUser)
        {
            return false;
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}