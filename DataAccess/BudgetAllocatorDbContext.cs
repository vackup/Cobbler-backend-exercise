using DataAccess.Maps;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BudgetAllocatorDbContext : DbContext
    {
        public BudgetAllocatorDbContext(DbContextOptions<BudgetAllocatorDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new BugetMap(modelBuilder.Entity<Budget>());
            new MoneyAllocationMap(modelBuilder.Entity<MoneyAllocation>());
            new PersonMap(modelBuilder.Entity<Person>());
            new ProjectMap(modelBuilder.Entity<Project>());
        }
    }
}