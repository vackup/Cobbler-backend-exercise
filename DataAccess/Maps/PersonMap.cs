using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Maps
{
    public class PersonMap : BaseMap<Person, int>
    {
        public PersonMap(EntityTypeBuilder<Person> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}