using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Maps
{
    public class MoneyAllocationMap : BaseMap<MoneyAllocation, int>
    {
        public MoneyAllocationMap(EntityTypeBuilder<MoneyAllocation> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}