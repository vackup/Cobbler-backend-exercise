using System;

namespace DataAccess.Contracts
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}