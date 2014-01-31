using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;

namespace BookArena.DAL.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BookArenaDbContext _dbContext;

        public TransactionRepository()
        {
            _dbContext = new BookArenaDbContext();
        }

        public void InsertOrUpdate(Transaction entity)
        {
            if (entity.Id == default(int))
            {
                _dbContext.Transaction.Add(entity);
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transaction> FindAll(Expression<Func<Transaction, bool>> predicate)
        {
            return _dbContext.Transaction.Where(predicate);
        }

        public Transaction Find(Expression<Func<Transaction, bool>> predicate)
        {
            return _dbContext.Transaction.Where(predicate).FirstOrDefault();
        }

        public IQueryable<Transaction> FindAll()
        {
            return _dbContext.Transaction;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}