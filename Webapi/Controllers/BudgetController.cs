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
        private readonly IBudgetBusiness budgetBusiness;

        public BudgetController(IBudgetBusiness business)
        {
            this.budgetBusiness = business ?? throw new ArgumentNullException(nameof(business));
        }

        /// <summary>
        /// Gets how much money is available to allocate to a user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<decimal> Get()
        {
            return await this.budgetBusiness.GetAvailableMoneyToAllocateByUserIdAsync(HelperData.User);
        }

        // GET api/<BudgetController>/5
        [HttpGet("{id}")]
        public async Task<Budget> Get(int id)
        {
            return await this.budgetBusiness.GetAsync(id);
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
                InitialMoneyToAllocate = 10000m,
                CreationDate = DateTime.Now
            };

            await this.budgetBusiness.CreateAsync(budget);
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

            await this.budgetBusiness.UpdateAsync(author);
        }

        // DELETE api/<BudgetController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.budgetBusiness.DeleteAsync(id);
        }
    }
}
