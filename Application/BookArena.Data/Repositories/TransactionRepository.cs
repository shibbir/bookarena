﻿using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.Data.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
    }
}