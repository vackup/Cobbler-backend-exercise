using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Business.Contracts
{
    public interface IBookBusiness
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book> GetAsync(int id);

        Task<Book> GetFirstOrDefaultAsync();

        Task InsertAsync(Book book);

        Task UpdateAsync(Book book);

        Task DeleteAsync(int id);

        Task DeleteAsync(Book book);

        Task SellBookAsync(int id);
    }
}
