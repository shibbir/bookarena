using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BookArena.DAL.Interfaces;
using BookArena.Model.EntityModel;
using BookArena.Model.ViewModel;

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
            throw new NotImplementedException();
        }

        public Transaction Find(Expression<Func<Transaction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transaction> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactionViewModel> Transactions()
        {
            var transactions = _dbContext.Transaction.ToList();
            Mapper.CreateMap<Transaction, TransactionViewModel>();
            return Mapper.Map<List<Transaction>, List<TransactionViewModel>>(transactions);
        }

        public IEnumerable<TransactionViewModel> Transactions(Expression<Func<Transaction, bool>> predicate)
        {
            var transactions = _dbContext.Transaction.Where(predicate).ToList();
            Mapper.CreateMap<Transaction, TransactionViewModel>();
            return Mapper.Map<List<Transaction>, List<TransactionViewModel>>(transactions);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}