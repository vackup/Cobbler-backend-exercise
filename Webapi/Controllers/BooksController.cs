using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using Entities;
using Webapi.Models;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookBusiness business;

        public BooksController(IBookBusiness business)
        {
            this.business = business ?? throw new ArgumentNullException(nameof(business));
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<BookResponseModel>> Get()
        {
            var books = (await this.business.GetAllAsync()).ToList();

            return !books.Any() ? new List<BookResponseModel>() : books.Select(this.ToBookResponseModel);
        }

        private BookResponseModel ToBookResponseModel(Book book)
        {
            return new BookResponseModel
            {
                Id = book.Id,
                SalesCount = book.SalesCount,
                Title = book.Title,
                Author = new AuthorResponseModel
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                }
            };
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await this.business.GetAsync(id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task Post([FromBody] BookCreationModel bookCreation)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            await this.business.InsertAsync(GetEntity(bookCreation));
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] BookCreationModel bookCreation)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            bookCreation.Id = id;

            await this.business.UpdateAsync(GetEntity(bookCreation));
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.business.DeleteAsync(id);
        }

        private Book GetEntity(BookCreationModel bookCreation)
        {
            return new Book
            {
                Id = bookCreation.Id,
                Author = new Budget { Id = bookCreation.AuthorId },
                Title = bookCreation.Title
            };
        }
    }
}
