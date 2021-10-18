using System;
using System.Threading.Tasks;
using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BudgetAllocatorDbContext context;

        public IBudgetRepository BudgetRepository { get; }
        public IMoneyAllocationRepository MoneyAllocationRepository { get; }
        public IPersonRepository PersonRepository { get; }
        public IProjectRepository ProjectRepository { get; }

        public UnitOfWork(BudgetAllocatorDbContext context)
        {
            this.context = context;
            this.BudgetRepository = new BudgetRepository(this.context);
            this.MoneyAllocationRepository = new MoneyAllocationRepository(this.context);
            this.PersonRepository = new PersonRepository(this.context);
            this.ProjectRepository = new ProjectRepository(this.context);
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(this.context);
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}