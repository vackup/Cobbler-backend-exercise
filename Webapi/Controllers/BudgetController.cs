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
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetBusiness business;

        public BudgetController(IBudgetBusiness business)
        {
            this.business = business ?? throw new ArgumentNullException(nameof(business));
        }

        // GET: api/<BudgetController>
        [HttpGet]
        public async Task<IEnumerable<Budget>> Get()
        {
            return await this.business.GetAllAsync();
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public async Task<Budget> Get(int id)
        {
            return await this.business.GetAsync(id);
        }

        /// <summary>
        /// Creates new budget for a user. 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task Post()
        {
            var budget = new Budget
            { 
                User = HelperData.User,
                MoneyToAllocate = 10000m,
                CreationDate = DateTime.Now
            };

            await this.business.CreateAsync(budget);
        }

        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Budget author)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            author.Id = id;

            await this.business.UpdateAsync(author);
        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.business.DeleteAsync(id);
        }
    }
}
