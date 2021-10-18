using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Maps
{
    public class BugetMap : BaseMap<Budget, int>
    {
        public BugetMap(EntityTypeBuilder<Budget> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}