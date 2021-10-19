using System;
using Entities;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IBudgetRepository : IRepository<Budget, Guid>
    {
        Task<Budget> GetByUserIdAsync(int userId);
        Task<Budget> GetIncludeMoneyAllocationsByUserIdAsync(int userId);
    }
}