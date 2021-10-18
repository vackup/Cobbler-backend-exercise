using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess
{
    public class EntityDatabaseTransaction : IDatabaseTransaction
    {
        // private readonly IDbContextTransaction transaction;

        public EntityDatabaseTransaction(BudgetAllocatorDbContext context)
        {
            // Transactions are not supported by the in-memory store. Just for DEMO
            // this.transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
           // this.transaction.Commit();
        }

        public void Rollback()
        {
            // this.transaction.Rollback();
        }

        public void Dispose()
        {
            // this.transaction.Dispose();
        }
    }
}