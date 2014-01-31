using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

namespace BookArena.DAL.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        IEnumerable<TransactionViewModel> Transactions();
        IEnumerable<TransactionViewModel> Transactions(Expression<Func<Transaction, bool>> predicate);
    }
}