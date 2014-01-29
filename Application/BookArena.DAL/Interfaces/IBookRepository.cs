using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<BasicBookViewModel> LatestBooks(int limit);
        IEnumerable<TransactionViewModel> Transactions();
        IEnumerable<TransactionViewModel> Transactions(Expression<Func<Transaction, bool>> predicate);
        void SaveTransactions(Transaction transaction);
    }
}