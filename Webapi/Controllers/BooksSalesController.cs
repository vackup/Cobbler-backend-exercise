using System;
using System.Threading.Tasks;
using Business.Contracts;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksSalesController : ControllerBase
    {
        private readonly IBookBusiness business;

        public BooksSalesController(IBookBusiness business)
        {
            this.business = business ?? throw new ArgumentNullException(nameof(business));
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task Post([FromBody] BookSaleModel bookSaleModel)
        {
            if (!this.ModelState.IsValid)
            {
                throw new Exception();
            }

            await this.business.SellBookAsync(bookSaleModel.Id);
        }
    }
}