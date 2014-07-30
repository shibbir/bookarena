﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository()
        {
            _dbContext = new ApplicationDbContext();
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

        public void Save()
        {
            _dbContext.SaveChanges();
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
    }
}