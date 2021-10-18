using System;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class DataGenerator
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookLibraryDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookLibraryDbContext>>()))
            {
                var authorRepository = new BudgetRepository(context);
                var bookRepository = new BookRepository(context);

                var author = new Budget { Id = 1, Name = "Borges" };

                await authorRepository.InsertAsync(author);

                await bookRepository.InsertAsync(new Book { Id = 1, Author = author, Title = "Book 1"});
                await bookRepository.InsertAsync(new Book { Id = 2, Author = author, Title = "Book 2" });
                await bookRepository.InsertAsync(new Book { Id = 3, Author = author, Title = "Book 3" });
                await bookRepository.InsertAsync(new Book { Id = 4, Author = author, Title = "Book 4" });
                await bookRepository.InsertAsync(new Book { Id = 5, Author = author, Title = "Book 5" });
            }
        }
    }
}