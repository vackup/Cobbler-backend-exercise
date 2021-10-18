using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Contracts;
using DataAccess.Contracts;
using Entities;

namespace Business
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepository bookRepository;
        private readonly IBudgetRepository authorRepository;

        public BookBusiness(IBookRepository bookRepository, IBudgetRepository authorRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await this.bookRepository.GetAllAsync();
        }

        public async Task<Book> GetAsync(int id)
        {
            return await this.bookRepository.GetAsync(id);
        }

        public async Task<Book> GetFirstOrDefaultAsync()
        {
            return await this.bookRepository.GetFirstOrDefaultAsync();
        }

        public async Task InsertAsync(Book book)
        {
            if (book.Author.Id > 0)
            {
                var author = await this.authorRepository.GetAsync(book.Author.Id);

                if (author == null)
                {
                    throw new Exception("The author you are trying to assing to the book doesn't exist.");
                }

                book.Author = author;
            }
            else
            {
                book.Author = null;
            }

            await this.bookRepository.InsertAsync(book);
        }

        public async Task UpdateAsync(Book book)
        {
            await this.bookRepository.UpdateAsync(book);
        }

        public async Task DeleteAsync(int id)
        {
            await this.bookRepository.DeleteAsync(id);
        }

        public async Task DeleteAsync(Book book)
        {
            await this.bookRepository.DeleteAsync(book);
        }

        public async Task SellBookAsync(int id)
        {
            var book = await this.GetAsync(id);

            if (book == null)
            {
                throw new Exception("You are trying to sell a book that doesn't exist.");
            }

            book.SalesCount++;

            await this.UpdateAsync(book);
        }
    }
}
