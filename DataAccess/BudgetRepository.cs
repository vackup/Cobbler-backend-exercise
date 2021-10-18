using DataAccess.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BudgetRepository : Repository<Budget, int>, IBudgetRepository
    {
        public BudgetRepository(BookLibraryDbContext context) : base(context)
        {
        }

        public async Task<Budget> GetByUserIdAsync(int userId)
        {
            return await this.Entities.SingleOrDefaultAsync(e => e.User == userId);
        }
    }
}