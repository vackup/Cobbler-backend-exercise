using Entities;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IBudgetRepository : IRepository<Budget, int>
    {
        Task<Budget> GetByUserIdAsync(int userId);
    }
}