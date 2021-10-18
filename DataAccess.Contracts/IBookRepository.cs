using System;
using Entities;

namespace DataAccess.Contracts
{
    public interface IBookRepository : IRepository<Book, int>
    {
    }
}
