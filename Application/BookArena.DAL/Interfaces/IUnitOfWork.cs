using System;

namespace BookArena.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}