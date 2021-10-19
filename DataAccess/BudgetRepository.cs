using System;
using DataAccess.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BudgetRepository : Repository<Budget, Guid>, IBudgetRepository
    {
        public BudgetRepository(BudgetAllocatorDbContext context) : base(context)
        {
        }

        public async Task<Budget> GetByUserIdAsync(int userId)
        {
            return await this.Entities.SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<Budget> GetIncludeMoneyAllocationsByUserIdAsync(int userId)
        {
            return await this.Entities
                .Include(b => b.MoneyAllocations)
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }
    }
}