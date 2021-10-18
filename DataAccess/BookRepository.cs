using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BookRepository : Repository<Book, int>, IBookRepository
    {
        public BookRepository(BookLibraryDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await this.Entities
                .Include(b => b.Author)
                .ToListAsync();
        }
    }
}