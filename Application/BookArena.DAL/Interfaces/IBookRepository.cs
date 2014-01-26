using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<Category> Categories();
        IEnumerable<BasicBookViewModel> LatestBooks(int limit);
        IQueryable<Transaction> Transactions(Expression<Func<Transaction, bool>> predicate);
        void SaveTransactions(Transaction transaction);
    }
}