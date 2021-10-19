using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Business.Contracts;
using Entities;
using Webapi.Helpers;

namespace Webapi.Controllers
{
    /// <summary>
    /// Controller for Budget operations
    /// </summary>
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

        /// <summary>
        /// Creates new budget for a user. 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task Post()
        {
            var budget = new Budget
            { 
                UserId = HelperData.User,
                InitialMoneyToAllocate = 10000m,
                CreationDate = DateTime.Now
            };

            await this.budgetBusiness.CreateNewBudgetAsync(budget);
        }
    }
}
