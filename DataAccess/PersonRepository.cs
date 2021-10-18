using DataAccess.Contracts;
using Entities;

namespace DataAccess
{
    public class PersonRepository : Repository<Person, int>, IPersonRepository
    {
        public PersonRepository(BudgetAllocatorDbContext context) : base(context)
        {
        }
    }
}