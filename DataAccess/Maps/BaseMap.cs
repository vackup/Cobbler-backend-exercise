using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Maps
{
    public abstract class BaseMap<T, TKey> where T : BaseEntity<TKey>
    {
        protected BaseMap(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }
}