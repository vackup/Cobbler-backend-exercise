using System;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Maps
{
    public class BudgetMap : BaseMap<Budget, Guid>
    {
        public BudgetMap(EntityTypeBuilder<Budget> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}