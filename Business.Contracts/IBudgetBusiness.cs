using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Business.Contracts
{
    public interface IBudgetBusiness
    {
        Task<IEnumerable<Budget>> GetAllAsync();

        Task<Budget> GetAsync(int id);

        Task<Budget> GetFirstOrDefaultAsync();

        /// <summary>
        /// Creates new budget for a user. 
        /// </summary>
        Task CreateAsync(Budget entity);

        Task UpdateAsync(Budget entity);

        Task DeleteAsync(int id);

        Task DeleteAsync(Budget entity);
    }
}