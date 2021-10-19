using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MoneyAllocationRepository : Repository<MoneyAllocation, Guid>, IMoneyAllocationRepository
    {
        public MoneyAllocationRepository(BudgetAllocatorDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MoneyAllocation>> GetIncludePersonAndProjectByBudgetIdAsync(Guid budgetId)
        {
            return await this.Entities
                .Include(ma => ma.Project)
                .Include(ma => ma.Person)
                .Where(ma => ma.BudgetId == budgetId)
                .ToListAsync();
        }
    }
}