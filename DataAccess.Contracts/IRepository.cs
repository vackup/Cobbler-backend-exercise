using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(TKey id);

        Task<TEntity> GetFirstOrDefaultAsync();

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TKey id);

        Task DeleteAsync(TEntity entity);
    }
}