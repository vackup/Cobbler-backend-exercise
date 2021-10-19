using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Business.Contracts
{
    public interface IBudgetBusiness
    {
        /// <summary>
        /// Creates new budget for a user. 
        /// </summary>
        Task CreateNewBudgetAsync(Budget entity);

        /// <summary>
        /// Updates allocations that I have already made.
        /// </summary>
        /// <param name="moneyAllocation"></param>
        /// <returns></returns>
        Task UpdateMoneyAllocationAsync(MoneyAllocation moneyAllocation);

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
        Task<Guid> CreateNewMoneyAllocationAsync(MoneyAllocation moneyAllocation);

        /// <summary>
        /// Gets all Money Allocations made By a UserId
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<MoneyAllocation>> GetMoneyAllocationsByUserIdAsync(int user);
    }
}