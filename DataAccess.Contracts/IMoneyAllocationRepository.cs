using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Contracts
{
    public interface IMoneyAllocationRepository : IRepository<MoneyAllocation, Guid>
    {
        Task<IEnumerable<MoneyAllocation>> GetIncludePersonAndProjectByBudgetIdAsync(Guid budgetId);
    }
}
