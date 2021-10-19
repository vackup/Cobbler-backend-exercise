using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Business.Contracts
{
    public interface IBudgetBusiness
    {
        Task<IEnumerable<Budget>> GetAllAsync();

        Task<Budget> GetAsync(Guid id);

        Task<Budget> GetFirstOrDefaultAsync();

        /// <summary>
        /// Creates new budget for a user. 
        /// </summary>
        Task CreateNewBudgetAsync(Budget entity);

        Task UpdateAsync(Budget entity);

        Task DeleteAsync(Guid id);

        Task DeleteAsync(Budget entity);

        /// <summary>
        /// Gets Available Money To Allocate By UserId
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<decimal> GetAvailableMoneyToAllocateByUserIdAsync(int user);

        /// <summary>
        /// Create New Money Allocation
        /// </summary>
        /// <param name="moneyAllocation"></param>
        /// <returns></returns>
        Task CreateNewMoneyAllocationAsync(MoneyAllocation moneyAllocation);

        /// <summary>
        /// Gets all Money Allocations made By a UserId
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<MoneyAllocation>> GetMoneyAllocationsByUserIdAsync(int user);
    }
}