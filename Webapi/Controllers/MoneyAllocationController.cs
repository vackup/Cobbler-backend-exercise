using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using Webapi.Helpers;
using Webapi.Models;

namespace Webapi.Controllers
{
    /// <summary>
    /// Controller for Money Allocation operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyAllocationController : ControllerBase
    {
        private readonly IBudgetBusiness budgetBusiness;

        public MoneyAllocationController(IBudgetBusiness business)
        {
            this.budgetBusiness = business ?? throw new ArgumentNullException(nameof(business));
        }

        /// <summary>
        /// View the allocations grouped by person
        /// </summary>
        /// <returns></returns>
        [Route("~/GroupByPerson")]
        [HttpGet]
        public async Task<IEnumerable<MoneyAllocationsGroupByPersonModel>> GetGroupByPerson()
        {
            var moneyAllocations = (await this.budgetBusiness.GetMoneyAllocationsByUserIdAsync(HelperData.User)).ToList();

            return !moneyAllocations.ToList().Any() ? null : HelperMappings.EntityToMoneyAllocationsGroupByPersonModel(moneyAllocations);
        }

        /// <summary>
        /// View the allocations grouped by project
        /// </summary>
        /// <returns></returns>
        [Route("~/GroupByProject")]
        [HttpGet]
        public async Task<IEnumerable<MoneyAllocationsGroupByProjectModel>> GetGroupByProject()
        {
            var moneyAllocations = (await this.budgetBusiness.GetMoneyAllocationsByUserIdAsync(HelperData.User)).ToList();

            return !moneyAllocations.ToList().Any() ? null : HelperMappings.EntityToMoneyAllocationsGroupByProjectModel(moneyAllocations);
        }

        /// <summary>
        /// Allocates money for a user to a project and / or person
        /// </summary>
        /// <param name="moneyAllocationModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> Post([FromBody] MoneyAllocationModel moneyAllocationModel)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            return await this.budgetBusiness.CreateNewMoneyAllocationAsync(HelperMappings.MoneyAllocationCreationModelToEntity(moneyAllocationModel, HelperData.User));
        }

        /// <summary>
        /// Updates allocations that I have already made.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moneyAllocationModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] MoneyAllocationModel moneyAllocationModel)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            await this.budgetBusiness.UpdateMoneyAllocationAsync(HelperMappings.MoneyAllocationCreationModelToEntity(moneyAllocationModel, id, HelperData.User));
        }
    }
}
