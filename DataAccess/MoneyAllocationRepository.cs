using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MoneyAllocationRepository : Repository<MoneyAllocation, int>, IMoneyAllocationRepository
    {
        public MoneyAllocationRepository(BudgetAllocatorDbContext context) : base(context)
        {
        }
    }
}