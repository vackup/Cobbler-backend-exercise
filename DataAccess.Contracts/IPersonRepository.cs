using Entities;

namespace DataAccess.Contracts
{
    public interface IPersonRepository : IRepository<Person, int>
    {
    }
}