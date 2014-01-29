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
    public class BookRepository : IBookRepository
    {
        private readonly BookArenaDbContext _dbContext;

        public BookRepository()
        {
            _dbContext = new BookArenaDbContext();
        }

        public void InsertOrUpdate(Book entity)
        {
            if (entity.BookId == default(int))
            {
                _dbContext.Book.Add(entity);
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

        public IQueryable<Book> FindAll(Expression<Func<Book, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Book Find(Expression<Func<Book, bool>> predicate)
        {
            return _dbContext.Book.Where(predicate).FirstOrDefault();
        }

        public IQueryable<Book> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BasicBookViewModel> LatestBooks(int limit)
        {
            var latestBooks = _dbContext.Book.Select(book => new BasicBookViewModel
            {
                Id = book.BookId,
                Title = book.Title,
                ImageFileName = book.ImageFileName,
                AvailableQuantity = book.AvailableQuantity
            }).OrderByDescending(x => x.Id).Take(limit).ToList();
            return latestBooks;
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

        public void SaveTransactions(Transaction transaction)
        {
            _dbContext.Transaction.Add(transaction);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}