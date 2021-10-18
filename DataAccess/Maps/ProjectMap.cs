using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Maps
{
    public class ProjectMap : BaseMap<Project, int>
    {
        public ProjectMap(EntityTypeBuilder<Project> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}