using DataAccess.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BudgetRepository : Repository<Budget, int>, IBudgetRepository
    {
        public BudgetRepository(BudgetAllocatorDbContext context) : base(context)
        {
        }

        public async Task<Budget> GetByUserIdAsync(int userId)
        {
            return await this.Entities.SingleOrDefaultAsync(e => e.User == userId);
        }

        public async Task<Budget> GetIncludeMoneyAllocationsByUserIdAsync(int userId)
        {
            return await this.Entities
                .Include(b => b.MoneyAllocations)
                .SingleOrDefaultAsync(e => e.User == userId);
        }
    }
}