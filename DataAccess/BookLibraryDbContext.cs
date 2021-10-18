using DataAccess.Maps;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BookLibraryDbContext : DbContext
    {
        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new AuthorMap(modelBuilder.Entity<Budget>());
            new BookMap(modelBuilder.Entity<Book>());
        }
    }
}